using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Resolvers;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class ResponseGraphType<TGraphType> : ObjectGraphType<Response> where TGraphType : GraphType
    {
        public ResponseGraphType()
        {
            Name = $"Response{typeof(TGraphType).Name}";

            Field(x => x.StatusCode, nullable: true).Description("Status code of the request.");
            Field(x => x.ErrorMessage, nullable: true).Description("Error message if requests fails.");

            Field<TGraphType>(
                "data",
                "Data returned by query.",
                resolve: context => context.Source.Data
            );
        }
    }
}
