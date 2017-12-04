using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.CORE.Data;
using AutoMapper;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class MapType : ObjectGraphType<MapDto>
    {
        private readonly IPlayerRepository _playerRepository;

        public MapType(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            Field(x => x.MapId, type: typeof(IdGraphType)).Description("The ID of the Map.");
            Field(x => x.Name, nullable: false).Description("The name of the Map");
            Field(x => x.MaxMoves, nullable: false).Description("MaxMoves for Map");
            FieldAsync<ListGraphType<RoundType>>(
                "rounds",
                "Rounds played on this map.",
                resolve: async context =>
                {
                    var roundsFromRepo = await _playerRepository.GetAllRoundsForMapAsync(context.Source.MapId);
                    var roundsToRetorn = Mapper.Map<IEnumerable<RoundDto>>(roundsFromRepo);
                    return roundsToRetorn;
                });
        }
    }
}
