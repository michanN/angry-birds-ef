using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using AngryBirds.CORE.Models;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class RoundType : ObjectGraphType<RoundDto>, IGraphType
    {
        public RoundType()
        {
            Field(r => r.RoundId, type: typeof(IdGraphType)).Description("The ID of round");
            Field(r => r.PlayerId, type: typeof(IdGraphType)).Description("The PlayerId connected to Round");
            Field(r => r.MapId, type: typeof(IdGraphType)).Description("The MapId connected to Round");
            Field(r => r.Points).Description("Points for Round");
        }
    }
}
