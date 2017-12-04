using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngryBirds.API.Models;
using AngryBirds.CORE.Data;
using AngryBirds.DATA.Repositories;
using GraphQL.Types;
using GraphQL;
using AngryBirds.API.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public async Task<object> PostAsync([FromBody]GraphQLQuery query)
            => await _processor.ProcessAsync(query);

        //private AngryBirdsQuery _angryBirdsQuery { get; set; }
        //private ISchema _schema { get; set; }
        //private IDocumentExecuter _documentExecuter { get; set; }

        //public GraphQLController(AngryBirdsQuery angryBirdsQuery, ISchema schema, IDocumentExecuter documentExecuter)
        //{
        //    _angryBirdsQuery = angryBirdsQuery;
        //    _schema = schema;
        //    _documentExecuter = documentExecuter;
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post(HttpRequestMessage request, GraphQLQuery query)
        //{
        //    try
        //    {
        //        var inputs = query.Variables.ToInputs();
        //        var queryToExecute = query.Query;

        //        var result = await _documentExecuter.ExecuteAsync(_ =>
        //        {
        //            _.Schema = _schema;
        //            _.Query = queryToExecute;
        //            _.Inputs = inputs;

        //        }).ConfigureAwait(false);

        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}
    }
}
