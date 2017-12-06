using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AngryBirds.CLIENT.Models;
using Newtonsoft.Json.Linq;

namespace AngryBirds.CLIENT
{
    public class App
    {
        private readonly string _url;
        private GraphQLClient _graphQlClient;

        public IList<Player> Players { get; private set; } = new List<Player>();
        public IList<Map> Maps { get; private set; } = new List<Map>();
        public IList<Round> Rounds { get; private set; } = new List<Round>();
        public IDictionary<string, string> Queries = new Dictionary<string, string>();

        private Player ActiveUser;

        public App(string url)
        {
            Thread.Sleep(5000);
            _url = url;
            _graphQlClient = new GraphQLClient(_url);
            InitializeApp();
        }

        private void InitializeApp()
        {
            CreateQueries();
            RunQueriesAtStartUp();
        }

        private void RunQueriesAtStartUp()
        {
            GetAllPlayers();
            GetAllMaps();
            GetAllRounds();
        }

        private void CreateQueries()
        {
            var getPlayerByName = @"query($name: String!) { playerByName(name: $name) { statusCode errorMessage data { playerId name rounds { roundId playerId mapId points } }}}";
            Queries.Add("getPlayerByName", getPlayerByName);

            var getAllPlayers = @"query { players { statusCode errorMessage data { playerId name rounds { roundId playerId mapId points }}}}";
            Queries.Add("getAllPlayers", getAllPlayers);

            var getAllMaps = @"query { maps { statusCode errorMessage data { mapId maxMoves name rounds { roundId playerId mapId points }}}}";
            Queries.Add("getAllMaps", getAllMaps);

            var getAllRounds = @"query { rounds { statusCode errorMessage data { roundId playerId mapId points } } }";
            Queries.Add("getAllRounds", getAllRounds);

        }

        public void GetAllPlayers()
        {
            var jsonObj = _graphQlClient.Query(Queries["getAllPlayers"], null).Get("players");
            var players = ParseJsonToObjects<Player>(jsonObj);
            Players = players;
        }

        public void GetAllMaps()
        {
            var jsonObj = _graphQlClient.Query(Queries["getAllMaps"], null).Get("maps");
            var maps = ParseJsonToObjects<Map>(jsonObj);
            Maps = maps;
        }

        public void GetAllRounds()
        {
            var jsonObj = _graphQlClient.Query(Queries["getAllRounds"], null).Get("rounds");
            var rounds = ParseJsonToObjects<Round>(jsonObj);
            Rounds = rounds;
        }

        private IList<T> ParseJsonToObjects<T>(dynamic obj)
        {
            IList<JToken> results = obj["data"];

            IList<T> objects = new List<T>();
            foreach (JToken result in results)
            {
                T appObj = result.ToObject<T>();
                objects.Add(appObj);
            }

            return objects;
        }



        public void LoginUser()
        {
            Console.WriteLine("Enter your name: ");
            var input = Console.ReadLine();

        }
    }

    //1. Programmet ska skriva ut innehållet i tabellerna.
        //1.1 När poängen på en bana visas så ska programmet dessutom visa vilken spelare som har den bästa poängen på banan (Exempel: "Bana 1: 3 drag (0 kvar) >> David: 1 drag")
    //2. Spelaren ska kunna skriva in sitt namn
        //2.1 Programmet ska känna igen om namnet finns i databasen
        //2.1.1 Om namnet finns ska programmet skriva ut en lista på alla banor som spelaren klarat, hur många drag på varje bana, hur många drag som spelaren inte behövde använda och hur många drag spelaren förbrukat på alla banor sammanlagt.
            //Exempel: "Bana 1: 2 drag (1 kvar) \n Bana 2: 3 drag (0 kvar) \n 5 drag totalt."
        //2.2 Om namnet inte finns ska det läggas till i databasen.
    //3. Spelaren ska kunna uppdatera sin poäng på någon bana.
        //3.1 Om spelaren vill det ska programmet fråga efter banans id-nummer och den nya poängen, och uppdatera databasen.
    //4. Spelaren ska kunna lägga till en bana.

}
