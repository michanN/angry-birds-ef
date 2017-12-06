using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Errors
{
    public class NotFoundError : GraphQLError
    {

        public NotFoundError(string id) : base(nameof(NotFoundError), $"Resource with {id} not found")
        {
        }
    }
}
