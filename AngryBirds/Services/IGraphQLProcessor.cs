using AngryBirds.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Schemas;

namespace AngryBirds.API.Services
{
    public interface IGraphQLProcessor
    {
        Task<string> ProcessAsync(GraphQLParameter query);
    }
}
