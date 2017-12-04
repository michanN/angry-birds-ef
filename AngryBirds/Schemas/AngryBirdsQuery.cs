using AngryBirds.API.Models;
using AngryBirds.API.Types;
using AngryBirds.CORE.Data;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace AngryBirds.API.Schemas
{
    public class AngryBirdsQuery : ObjectGraphType<object>
    {
        private IPlayerRepository _playerRepository { get; set; }

        public AngryBirdsQuery(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            Name = "Query";

            FieldAsync<PlayerType>(
                "player",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "playerId" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("playerId");
                    var playerForRepo = await _playerRepository.GetByIdAsync(id);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;
                });

            FieldAsync<PlayerType>(
                "playerByName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");
                    var playerForRepo = await _playerRepository.GetByNameAsync(name);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;

                });

            FieldAsync<ListGraphType<PlayerType>>(
                "players",
                resolve: async context =>
                {
                    var playersFromRepo = await _playerRepository.GetAllPlayersAsync();
                    var playersToReturn = Mapper.Map<IEnumerable<PlayerDto>>(playersFromRepo);
                    return playersToReturn;
                });

            FieldAsync<MapType>(
                "map",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "mapId" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("mapId");
                    var mapFromRepo = await _playerRepository.GetMapByIdAsync(id);
                    var mapToRetun = Mapper.Map<MapDto>(mapFromRepo);
                    return mapToRetun;
                });

            FieldAsync<ListGraphType<MapType>>(
                "maps",
                resolve: async context =>
                {
                    var mapsFromRepo = await _playerRepository.GetAllMapsAsync();
                    var mapsToReturn = Mapper.Map<IEnumerable<MapDto>>(mapsFromRepo);
                    return mapsToReturn;
                });
        }
    }
}
