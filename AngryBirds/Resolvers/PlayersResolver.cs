using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Schemas;
using AngryBirds.CORE.Data;
using AngryBirds.API.Types;
using GraphQL.Types;
using AutoMapper;
using AngryBirds.API.Models;
using AngryBirds.CORE.Models;

namespace AngryBirds.API.Resolvers
{
    public class PlayersResolver : Resolver, IPlayersResolver
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersResolver(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void Resolve(GraphQLQuery graphQLQuery)
        {
            graphQLQuery.FieldAsync<ResponseGraphType<PlayerType>>(
                "player",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "playerId" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("playerId");
                    var playerForRepo = await _playerRepository.GetByIdAsync(id);

                    if (playerForRepo == null)
                    {
                        return NotFoundError(id.ToString());
                    }

                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return Response(playerToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<PlayerType>>(
                "playerByName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");
                    var playerForRepo = await _playerRepository.GetByNameAsync(name);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerForRepo);
                    return Response(playerToReturn);

                });

            graphQLQuery.FieldAsync<ResponseListGraphType<PlayerType>>(
                "players",
                resolve: async context =>
                {
                    var playersFromRepo = await _playerRepository.GetAllPlayersAsync();
                    var playersToReturn = Mapper.Map<IEnumerable<PlayerDto>>(playersFromRepo);
                    return Response(playersToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<PlayerType>>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" }
                ),
                resolve: async context =>
                {
                    var player = context.GetArgument<PlayerForCreationDto>("player");
                    Player playerFromRepo;

                    if (await _playerRepository.CheckIfPlayerNameExists(player.Name))
                    {
                        playerFromRepo = await _playerRepository.GetByNameAsync(player.Name);
                    }
                    else
                    {
                        var playerEntity = Mapper.Map<Player>(player);
                        playerFromRepo = await _playerRepository.AddPlayerAsync(playerEntity);
                    }

                    var playerToReturn = Mapper.Map<PlayerDto>(playerFromRepo);
                    return Response(playerToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<PlayerType>>(
                "updatePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" }
                ),
                resolve: async context =>
                {
                    var player = context.GetArgument<PlayerForManipulationDto>("player");
                    var playerEntity = Mapper.Map<Player>(player);
                    await _playerRepository.UpdatePlayerAsync(playerEntity);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerEntity);
                    return Response(playerToReturn);
                });
        }
    }
}
