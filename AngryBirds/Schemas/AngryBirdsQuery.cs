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

            Field<PlayerType>(
                "player",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "playerId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("playerId");
                    var playerForRepo = _playerRepository.Get(id).Result;
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;
                });

            Field<PlayerType>(
                "playerByName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "name"}),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var playerForRepo = _playerRepository.GetByName(name).Result;
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return playerToReturn;

                }); 

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context =>
                {
                    return _playerRepository.GetAll();
                });
        }
    }
}
