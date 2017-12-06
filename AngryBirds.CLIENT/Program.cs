using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using AngryBirds.CLIENT.Models;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngryBirds.CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:51918/api/";
            var app = new App(url);

            app.LoginUser();

            Console.ReadLine();

            //Test();
        }

        public static void Test()
        {
            var url = "http://localhost:51918/api/";
            var graphQLClient = new GraphQLClient(url);

            var queryAllMaps = @"
                query { maps { statusCode errorMessage data { mapId maxMoves name } } }
            ";

            var queryGetMap = @"
                query($mapId: String!) { 
                    map(mapId: $mapId) {
                        statusCode
                        errorMessage
	                    data {
	    	                mapId
		                    name
		                    maxMoves
	                    }
                    }
                }
            ";

            var obj = graphQLClient.Query(queryAllMaps, null).Get("maps");

            IList<JToken> results = obj["data"];

            Console.WriteLine(results);

            IList<Map> maps = new List<Map>();
            foreach (JToken result in results)
            {
                Map map = result.ToObject<Map>();
                maps.Add(map);
            }
        }
    }
}
