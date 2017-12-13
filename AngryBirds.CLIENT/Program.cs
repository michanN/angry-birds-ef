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
          
            Console.ReadLine();
        }
    }
}
