using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.API.Schemas;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Newtonsoft.Json.Linq;

namespace AngryBirds.API.Services
{
    public class GraphQLProcessor : IGraphQLProcessor
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public GraphQLProcessor(ISchema schema, IDocumentExecuter executer, IDocumentWriter writer)
        {
            _schema = schema;
            _executer = executer;
            _writer = writer;
        }

        public async Task<string> ProcessAsync(GraphQLQuery query)
        {
            Inputs inputs = null;
            if (query.Variables != null)
            {
                var variables = query.Variables as JObject;
                var values = GetValue(variables) as Dictionary<string, object>;
                inputs = new Inputs(values);
            }

            var queryToExecute = query.Query;
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = queryToExecute;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
            });

            return _writer.Write(result);
        }

        public static object GetValue(object value)
        {
            var objectValue = value as JObject;
            if (objectValue != null)
            {
                var output = new Dictionary<string, object>();
                foreach (var kvp in objectValue)
                {
                    output.Add(kvp.Key, GetValue(kvp.Value));
                }
                return output;
            }

            var propertyValue = value as JProperty;
            if (propertyValue != null)
            {
                return new Dictionary<string, object>
                {
                    { propertyValue.Name, GetValue(propertyValue.Value) }
                };
            }

            var arrayValue = value as JArray;
            if (arrayValue != null)
            {
                return arrayValue.Children().Aggregate(new List<object>(), (list, token) =>
                {
                    list.Add(GetValue(token));
                    return list;
                });
            }

            var rawValue = value as JValue;
            if (rawValue != null)
            {
                var val = rawValue.Value;
                if (val is long)
                {
                    long l = (long)val;
                    if (l >= int.MinValue && l <= int.MaxValue)
                    {
                        return (int)l;
                    }
                }
                return val;
            }

            return value;
        }
    }
}
