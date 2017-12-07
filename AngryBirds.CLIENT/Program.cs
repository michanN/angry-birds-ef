using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AngryBirds.CLIENT.Models;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngryBirds.CLIENT
{
    class Program
    {
        private static async Task Main(string[] args) 
        {
            var url = "http://localhost:51918/api/";
            var app = new App(url);

            await app.StartApp();
            //await app.LoginUser();
            //await app.CreateMap("Test", 29);
            //app.CreateMap("Pontus rum", 11); FUNKAR
            //app.GetRound(Guid.Parse("332F6B36-62EB-48E1-918D-F0D3DF2BB644")); FUNKAr
            //app.CreateRound(Guid.Parse("0bfe3dc5-47f3-4fec-8bb0-a1c22ddb5b96"), Guid.Parse("2d9b3b44-fcc5-47df-99e8-7771c25ab9a1"), 7);

            Console.ReadLine();

            //Test();
        }

        //public static void Test()
        //{
        //    var url = "http://localhost:51918/api/";
        //    var graphQLClient = new GraphQLClient(url);

        //    var queryAllMaps = @"
        //        query { maps { statusCode errorMessage data { mapId maxMoves name } } }
        //    ";

        //    var queryGetMap = @"
        //        query($mapId: String!) { 
        //            map(mapId: $mapId) {
        //                statusCode
        //                errorMessage
	       //             data {
	    	  //              mapId
		      //              name
		      //              maxMoves
	       //             }
        //            }
        //        }
        //    ";

        //    var obj = graphQLClient.Query(queryAllMaps, null).Get("maps");

        //    IList<JToken> results = obj["data"];

        //    Console.WriteLine(results);

        //    IList<Map> maps = new List<Map>();
        //    foreach (JToken result in results)
        //    {
        //        Map map = result.ToObject<Map>();
        //        maps.Add(map);
        //    }
        //}
    }
}
