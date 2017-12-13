using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.CORE.Data;
using AngryBirds.CORE.Models;
using AutoMapper;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class RoundType : ObjectGraphType<RoundDto>, IGraphType
    {
        private readonly IPlayerRepository _playerRepository;

        public RoundType(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            Field(r => r.RoundId, type: typeof(IdGraphType)).Description("The ID of round");
            Field(r => r.PlayerId, type: typeof(IdGraphType)).Description("The PlayerId connected to Round");
            Field(r => r.MapId, type: typeof(IdGraphType)).Description("The MapId connected to Round");
            Field(r => r.Points).Description("Points for Round");
            FieldAsync<PlayerType>(
                "player",
                "Player who played round.",
                resolve: async context =>
                {
                    var playerFromRepo = await _playerRepository.GetByIdAsync(context.Source.PlayerId);
                    var playerToReturn = Mapper.Map<PlayerDto>(playerFromRepo);
                    return playerToReturn;
                });
            FieldAsync<MapType>(
                "map",
                "Map which round was played on.",
                resolve: async context =>
                {
                    var mapFromRepo = await _playerRepository.GetMapByIdAsync(context.Source.MapId);
                    var mapToReturn = Mapper.Map<MapDto>(mapFromRepo);
                    return mapToReturn;
                });
        }
    }
}
