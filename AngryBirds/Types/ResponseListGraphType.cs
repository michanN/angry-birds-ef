using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Resolvers;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class ResponseListGraphType<TGraphType> : ObjectGraphType<Response> where TGraphType : GraphType
    {
        public ResponseListGraphType()
        {
            Name = $"ResponseList{typeof(TGraphType).Name}";

            Field(x => x.StatusCode, nullable: true).Description("Status code of the request.");
            Field(x => x.ErrorMessage, nullable: true).Description("Error message if requests fails.");

            Field<ListGraphType<TGraphType>>(
                "data",
                "Project data returned by query.",
                resolve: context => context.Source.Data
            );
        }
    }
}
