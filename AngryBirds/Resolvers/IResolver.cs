using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Schemas;

namespace AngryBirds.API.Resolvers
{
    public interface IResolver
    {
        void Resolve(GraphQLQuery graphQLQuery);
    }
}
