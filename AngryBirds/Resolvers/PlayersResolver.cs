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
            graphQLQuery.FieldAsync<PlayerType>(
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
        }
    }
}
