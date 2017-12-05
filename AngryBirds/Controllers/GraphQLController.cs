using AngryBirds.API.Schemas;
using AngryBirds.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngryBirds.Controllers
{
    [Route("api")]
    public class GraphQLController : Controller
    {
        private readonly IGraphQLProcessor _processor;

        public GraphQLController(IGraphQLProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost]
        public async Task<object> PostAsync([FromBody]GraphQLParameter query)
            => await _processor.ProcessAsync(query);
    }
}
