using System;
using System.Collections.Generic;
using AngryBirds.API.Models;
using AngryBirds.API.Schemas;
using AngryBirds.API.Types;
using AngryBirds.CORE.Data;
using AngryBirds.CORE.Models;
using AutoMapper;
using GraphQL.Types;

namespace AngryBirds.API.Resolvers
{
    public class RoundResolver : Resolver, IRoundResolver
    {
        private readonly IPlayerRepository _playerRepository;

        public RoundResolver(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void Resolve(GraphQLQuery graphQLQuery)
        {
            graphQLQuery.FieldAsync<ResponseGraphType<RoundType>>(
                "round",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "roundId"}
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("roundId");
                    var roundFromRepo = await _playerRepository.GetRoundById(id);
                    var roundToReturn = Mapper.Map<RoundDto>(roundFromRepo);
                    return Response(roundToReturn);
                });

            graphQLQuery.FieldAsync<ResponseListGraphType<RoundType>>(
                "rounds",
                resolve: async context =>
                {
                    var roundsFromRepo = await _playerRepository.GetAllRoundsAsync();
                    var roundsToReturn = Mapper.Map<IEnumerable<RoundDto>>(roundsFromRepo);
                    return Response(roundsToReturn);
                });

            graphQLQuery.FieldAsync<ResponseGraphType<RoundType>>(
                "createRound",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RoundInputType>> {Name = "round"}
                ),
                resolve: async context =>
                {
                    var round = context.GetArgument<RoundForCreationDto>("round");
                    var roundEntity = Mapper.Map<Round>(round);
                    var addedRound = await _playerRepository.AddRoundAsync(roundEntity);
                    var roundToReturn = Mapper.Map<RoundDto>(addedRound);
                    return Response(roundToReturn);
                });
        }
    }
}