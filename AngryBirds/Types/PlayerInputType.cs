﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngryBirds.API.Models;
using GraphQL.Types;

namespace AngryBirds.API.Types
{
    public class PlayerInputType : InputObjectGraphType
    {
        public PlayerInputType()
        {
            Name = "PlayerInput";

            Field<IdGraphType>("playerId");
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
