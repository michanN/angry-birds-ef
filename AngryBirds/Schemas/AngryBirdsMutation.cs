using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.CORE.Data;
using GraphQL.Types;
using AngryBirds.API.Models;
using AngryBirds.CORE.Models;
using AngryBirds.API.Types;
using AutoMapper;

namespace AngryBirds.API.Schemas
{
    public class AngryBirdsMutation : ObjectGraphType<PlayerType>
    {
        private readonly IPlayerRepository _playerRepository;

        public AngryBirdsMutation(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            FieldAsync<MapType>(
                "createMap",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MapInputType>> {Name = "map"}
                ),
                resolve: async context =>
                {
                    var map = context.GetArgument<MapForCreationDto>("map");
                    var mapEntity = Mapper.Map<Map>(map);
                    await _playerRepository.AddMapAsync(mapEntity);
                    var mapToReturn = Mapper.Map<MapDto>(mapEntity);
                    return mapToReturn;
                });

            FieldAsync<PlayerType>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" }
                ),
                resolve: async context =>
                {
                    var player = context.GetArgument<PlayerForCreationDto>("player");
                    var playerEntity = Mapper.Map<Player>(player);
                    await _playerRepository.AddPlayerAsync(playerEntity);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerEntity);
                    return playerToReturn;
                });

            FieldAsync<PlayerType>(
                "updatePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" }
                    //new QueryArgument<RoundInputType> { Name = "rounds" }
                ),
                resolve: async context =>
                {
                    //var rounds = context.GetArgument<IntGraphType>("rounds");
                    var player = context.GetArgument<PlayerForManipulationDto>("player");
                    var playerEntity = Mapper.Map<Player>(player);
                    await _playerRepository.UpdatePlayerAsync(playerEntity);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerEntity);
                    return playerToReturn;
                });

            //FieldAsync<RoundType>(
            //    "createRound",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<RoundInputType>> {Name = "round"}
            //    ),
            //    resolve: async context =>
            //    {
            //        var round = context.GetArgument<RoundForCreationDto>("round");
            //        var roundEntity = Mapper.Map<Round>(round);
            //        await _playerRepository.AddRoundAsync(roundEntity);
            //        var roundToReturn = Mapper.Map<RoundDto>(roundEntity);
            //        return roundToReturn;
            //    });
        }
    }
}
