﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private async void InitializeApp()
        {
            CreateQueries();
            await RunQueriesAtStartUp();
        }

        private async Task RunQueriesAtStartUp()
        {
            await GetAllPlayers();
            await GetAllMaps();
            await GetAllRounds();
        }

        private void CreateQueries()
        {
            var getPlayerByName = @"query($name: String!) { playerByName(name: $name) { statusCode errorMessage data { playerId name rounds { roundId playerId mapId points } }}}";
            Queries.Add("getPlayerByName", getPlayerByName);

            var getAllPlayers = @"query { players { statusCode errorMessage data { playerId name rounds { roundId playerId mapId points }}}}";
            Queries.Add("getAllPlayers", getAllPlayers);

            var createPlayer = @"mutation($player: PlayerInput!) { createPlayer(player: $player) { statusCode errorMessage data { playerId name rounds { roundId playerId mapId points } } } }";
            Queries.Add("createPlayer", createPlayer);

            var getAllMaps = @"query { maps { statusCode errorMessage data { mapId maxMoves name rounds { roundId playerId mapId points }}}}";
            Queries.Add("getAllMaps", getAllMaps);

            var createMap = @"mutation($map: MapInput!) { createMap(map: $map) { statusCode errorMessage data { mapId name maxMoves rounds { roundId playerId mapId points } } } }";
            Queries.Add("createMap", createMap);

            var getRoundById = @"query($roundId: String!) { round(roundId: $roundId) { statusCode errorMessage data { roundId playerId mapId points } } }";
            Queries.Add("getRoundById", getRoundById);

            var getAllRounds = @"query { rounds { statusCode errorMessage data { roundId playerId mapId points player { playerId name } map { mapId name maxMoves } }} }";
            Queries.Add("getAllRounds", getAllRounds);

            var createRound = @"mutation($round: RoundInput!) { createRound(round: $round) { statusCode errorMessage data { roundId playerId mapId points } } }";
            Queries.Add("createRound", createRound);

        }

        public async Task GetPlayer(string playerName)
        {
            dynamic jsonObj;

            if (Players.Any(x => x.Name == playerName))
            {
                dynamic jsonQuery = await _graphQlClient.Query(Queries["getPlayerByName"], new {name = playerName});
                jsonObj = jsonQuery.Get("playerByName");
            }
            else
            {
                dynamic jsonQuery = await _graphQlClient.Query(Queries["createPlayer"], new {player = new {name = playerName}});
                jsonObj = jsonQuery.Get("createPlayer");
            }

            var player = ParseJsonToObject<Player>(jsonObj);
            ActiveUser = player;
            Console.WriteLine($"{ActiveUser.Name}");
        }

        public async Task GetAllPlayers()
        {
            var jsonObj = await _graphQlClient.Query(Queries["getAllPlayers"], null);
            var getPlayers = jsonObj.Get("players");
            var players = ParseJsonToObjects<Player>(getPlayers);
            Players = players;
        }

        public async Task GetAllMaps()
        {
            var jsonObj = await _graphQlClient.Query(Queries["getAllMaps"], null);
            var getMaps = jsonObj.Get("maps");
            var maps = ParseJsonToObjects<Map>(getMaps);
            Maps = maps;
        }

        public async Task CreateMap(string mapName, int number)
        {
            var jsonObj = await _graphQlClient
                .Query(Queries["createMap"], new {map = new {name = mapName, maxMoves = number}});
            var getMap = jsonObj.Get("createMap");

            var map = ParseJsonToObject<Map>(getMap);
            if (map != null)
            {
                await GetAllMaps();
            }
        }

        public async Task GetRound(Guid id)
        {
            var jsonObj = await _graphQlClient.Query(Queries["getRoundById"], new {roundId = id});
            var getRound = jsonObj.Get("round");
            var round = ParseJsonToObject<Round>(getRound);
            Console.WriteLine("Round fetched");
        }

        public async Task CreateRound(Guid pId, Guid mId, int number)
        {
            var jsonObj = await _graphQlClient.Query(Queries["createRound"],
                new {round = new {playerId = pId, mapId = mId, points = number}});
            var getRound = jsonObj.Get("createRound");
            var round = ParseJsonToObject<Round>(getRound);
            Console.WriteLine("Round Added");
            if (round != null)
            {
                await GetAllRounds();
            }
        }

        public async Task GetAllRounds()
        {
            var jsonObj = await _graphQlClient.Query(Queries["getAllRounds"], null);
            var getRounds = jsonObj.Get("rounds");
            var rounds = ParseJsonToObjects<Round>(getRounds);
            Rounds = rounds;
        }

        public async Task LoginUser()
        {
            Console.WriteLine("Enter your name: ");
            var input = Console.ReadLine();

            await GetPlayer(input);
        }

        public void WriteUserRounds(Player player)
        {
            Console.Clear();
            foreach (var round in Rounds)
            {
                if (round.PlayerId == player.PlayerId)
                {
                    var highest = Rounds.Where(r => r.MapId == round.MapId).OrderBy(r => r.Points).Take(1);
                    Console.WriteLine($"Bana: {round.Map.Name} Använda Drag: {round.Points} Drag Kvar: {round.Map.MaxMoves - round.Points} HighScore {highest.First().Player.Name}: {highest.First().Points}");
                }
            }
            var totalscore = Rounds.Where(r => r.PlayerId == player.PlayerId).Sum(r => r.Points);
            Console.WriteLine($"Total antal moves: {totalscore}");

            Console.ReadLine();
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

        private T ParseJsonToObject<T>(dynamic obj)
        {
            JToken result = obj["data"];

            T appObj = result.ToObject<T>();

            return appObj;
        }

        public async Task StartApp()
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMenu();

                var validCommands = new string[] {"1", "2", "3", "4", "5", "q"};

                var userInput = Console.ReadLine().ToLower();
                if (validCommands.Contains(userInput))
                {
                    switch (userInput)
                    {
                        case "1":
                            await LoginUser();
                            WriteUserRounds(ActiveUser);
                            break;
                        case "2":
                            ShowAllMaps();
                            break;
                        case "3":
                            await MapCreation();
                            break;
                        case "4":
                            await PlayRound();
                            break;
                        case "5":
                            ListRounds();
                            break;
                        case "q":
                            exit = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command. Press ENTER to try again");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        public void ListRounds()
        {
            foreach (var round in Rounds)
            {
                Console.WriteLine($"{round.Player.Name} {round.Map.Name} {round.Points}");
            }
            Console.ReadLine();
        }

        public async Task PlayRound()
        {
            Console.Clear();
            Console.WriteLine("Please enter the GUID for the map:");
            var mapId = Console.ReadLine();
            Guid mapIdAsGuid = Guid.Parse(mapId);

            Console.WriteLine("Please enter your points: ");
            var points = Console.ReadLine();
            int pointsAsInt = Int32.Parse(points);

            await CreateRound(ActiveUser.PlayerId, mapIdAsGuid, pointsAsInt);
        }

        public async Task MapCreation()
        {
            Console.Clear();
            Console.WriteLine("Please enter a name for the map:");
            var mapName = Console.ReadLine();

            Console.WriteLine("Please enter number of max moves: ");
            var moves = Console.ReadLine();
            int movesAsInt = Int32.Parse(moves);

            await CreateMap(mapName, movesAsInt);
        }

        public void ShowAllMaps()
        {
            Console.Clear();
            foreach (var map in Maps)
            {
                Console.WriteLine($"Name: {map.Name} Max Moves: {map.MaxMoves}");
            }

            Console.ReadLine();
        }

        public void ShowMenu()
        {
            Console.WriteLine("Welcome to AngryBirds v.0.0.1");
            Console.WriteLine("Menu:");
            Console.WriteLine("[1]. Sign In");
            Console.WriteLine("[2]. Show Maps");
            Console.WriteLine("[3]. Add Map");
            if (ActiveUser != null)
            {
                Console.WriteLine("[4]. Play Round");
            }
            Console.WriteLine("[5]. List All Rounds");
            Console.WriteLine("");
            Console.WriteLine("[Q]uit");
            Console.WriteLine("");
        }
    }

    //1. Programmet ska skriva ut innehållet i tabellerna.
    //    1.1 När poängen på en bana visas så ska programmet dessutom visa vilken spelare som har den bästa poängen på banan(Exempel: "Bana 1: 3 drag (0 kvar) >> David: 1 drag")
    //2. Spelaren ska kunna skriva in sitt namn
    //    2.1 Programmet ska känna igen om namnet finns i databasen
    //    2.1.1 Om namnet finns ska programmet skriva ut en lista på alla banor som spelaren klarat, hur många drag på varje bana, hur många drag som spelaren inte behövde använda och hur många drag spelaren förbrukat på alla banor sammanlagt.
    //        Exempel: "Bana 1: 2 drag (1 kvar) \n Bana 2: 3 drag (0 kvar) \n 5 drag totalt."
    //    2.2 Om namnet inte finns ska det läggas till i databasen.
    //3. Spelaren ska kunna uppdatera sin poäng på någon bana.
    //    3.1 Om spelaren vill det ska programmet fråga efter banans id-nummer och den nya poängen, och uppdatera databasen.
    //4. Spelaren ska kunna lägga till en bana.

}
