using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class PlayerInputUpdateType : InputObjectGraphType
    {
        public PlayerInputUpdateType()
        {
            Name = "PlayerInputUpdate";

            Field<GraphType>("id");
            Field<GraphType>("name");
        }
    }
}
