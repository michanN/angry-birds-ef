using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngryBirds.CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            Test();
        }

        public static void Test()
        {
            var url = "http://localhost:51918/api/";
            var graphQLClient = new GraphQLClient(url);

            var query = @"
                query { maps { maxMoves name rounds { playerId points } } }
            ";

            var obj = graphQLClient.Query(query, null).Get("maps");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var content = 
            //HttpResponseMessage response = client.PostAsync(url, )

        }
    }
}
