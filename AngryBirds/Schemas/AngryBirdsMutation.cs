using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.CORE.Data;
using GraphQL.Types;
using AngryBirds.API.Models;
using AngryBirds.CORE.Models;
using AngryBirds.API.Types;

namespace AngryBirds.API.Schemas
{
    public class AngryBirdsMutation : ObjectGraphType<PlayerType>
    {
        private IPlayerRepository _playerRepository { get; set; }

        public AngryBirdsMutation(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            Field<PlayerType>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" }
                ),
                resolve: context =>
                {
                    var player = context.GetArgument<Player>("player");
                    return _playerRepository.AddPlayer(player);
                });
        }
    }
}
