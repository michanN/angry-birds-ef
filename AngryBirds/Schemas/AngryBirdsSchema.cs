using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;

namespace AngryBirds.API.Schemas
{
    public class AngryBirdsSchema : Schema
    {
        public AngryBirdsSchema(IDependencyResolver resolver)
            :base(resolver)
        {
            Query = resolver.Resolve<GraphQLQuery>();
            Mutation = resolver.Resolve<GraphQLQuery>();
        }
    }
}
