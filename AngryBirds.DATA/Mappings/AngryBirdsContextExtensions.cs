using AngryBirds.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngryBirds.DATA.Mappings
{
    public static class AngryBirdContextExtensions
    {
        public static void EnsureSeedDataForContext(this AngryBirdContext context)
        {
            context.Players.RemoveRange(context.Players);
            context.Maps.RemoveRange(context.Maps);
            context.Rounds.RemoveRange(context.Rounds);
            context.SaveChanges();

            var players = new List<Player>()
            {
                new Player()
                {
                    PlayerId = new Guid("0BFE3DC5-47F3-4FEC-8BB0-A1C22DDB5B96"),
                    Name = "Michel"
                },
                new Player()
                {
                    PlayerId = new Guid("42BA3637-5F3E-48BA-AEE9-C9703BBA0834"),
                    Name = "Erik"
                },
                new Player()
                {
                    PlayerId = new Guid("66529239-2EB7-4137-AA2E-6F19224E80C3"),
                    Name = "David"
                },
            };
            context.Players.AddRange(players);
            context.SaveChanges();

            var maps = new List<Map>()
            {
                new Map()
                {
                    MapId = new Guid("76FB89A7-9701-4C83-AE51-28A36D447174"),
                    Name = "The Dark Cave",
                    MaxMoves = 7
                },
                new Map()
                {
                    MapId = new Guid("2D9B3B44-FCC5-47DF-99E8-7771C25AB9A1"),
                    Name = "The Mysterious Forrest",
                    MaxMoves = 11
                },
                new Map()
                {
                    MapId = new Guid("072F6B36-62EB-48E1-918D-F0D3DF2BB631"),
                    Name = "Mr.Gonzo's Castle",
                    MaxMoves = 14
                },
            };
            context.Maps.AddRange(maps);
            context.SaveChanges();

            var rounds = new List<Round>()
            {
                new Round()
                {
                    RoundId = new Guid("112F6B36-62EB-48E1-918D-F0D3DF2BB622"),
                    PlayerId = players[0].PlayerId,
                    MapId = maps[0].MapId
                },
                new Round()
                {
                    RoundId = new Guid("332F6B36-62EB-48E1-918D-F0D3DF2BB644"),
                    PlayerId = players[0].PlayerId,
                    MapId = maps[1].MapId
                },
                new Round()
                {
                    RoundId = new Guid("552F6B36-62EB-48E1-918D-F0D3DF2BB666"),
                    PlayerId = players[1].PlayerId,
                    MapId = maps[2].MapId
                },
                new Round()
                {
                    RoundId = new Guid("772F6B36-62EB-48E1-918D-F0D3DF2BB688"),
                    PlayerId = players[2].PlayerId,
                    MapId = maps[1].MapId
                },
            };
            context.Rounds.AddRange(rounds);
            context.SaveChanges();
        }
    }
}
