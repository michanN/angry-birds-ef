using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class RoundInputType : InputObjectGraphType
    {
        public RoundInputType()
        {
            Name = "RoundInput";

            Field<IdGraphType>("playerId");
            Field<IdGraphType>("mapId");
            Field<IntGraphType>("points");
        }
    }
}
