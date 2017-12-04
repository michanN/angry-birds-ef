using AngryBirds.API.Models;
using AngryBirds.API.Types;
using AngryBirds.CORE.Data;
using GraphQL.Types;
using System;
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
                    var playerForRepo = await _playerRepository.Get(id);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;
                });

            FieldAsync<PlayerType>(
                "playerByName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "name"}),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");
                    var playerForRepo = await _playerRepository.GetByName(name);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;

                });

            FieldAsync<ListGraphType<PlayerType>>(
                "players",
                resolve: async context =>
                {
                    return await _playerRepository.GetAll();
                });
        }
    }
}
