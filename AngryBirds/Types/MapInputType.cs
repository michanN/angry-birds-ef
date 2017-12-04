using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class MapInputType : InputObjectGraphType
    {
        public MapInputType()
        {
            Name = "MapInput";

            Field<IdGraphType>("mapId");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("maxMoves");
        }
    }
}
