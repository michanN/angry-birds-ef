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
    public class PlayerType : ObjectGraphType<PlayerDto>
    {
        private IPlayerRepository _playerRepository;

        public PlayerType(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            Field(x => x.PlayerId, type: typeof(IdGraphType)).Description("The ID of the Player.");
            Field(x => x.Name, nullable: false).Description("The name of the Player");
            FieldAsync<ListGraphType<RoundType>>(
                "rounds",
                "Rounds played by player.",
                resolve: async context =>
                {
                    var roundsFromRepo = await _playerRepository.GetAllRounds(context.Source.PlayerId);
                    var roundsToRetorn = Mapper.Map<IEnumerable<RoundDto>>(roundsFromRepo);
                    return roundsToRetorn;
                });
        }
    }
}
