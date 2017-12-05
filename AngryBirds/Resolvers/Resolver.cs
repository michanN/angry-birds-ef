using AngryBirds.API.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Resolvers
{
    public class Resolver
    {
        public Response Response(object data)
        {
            return new Response(data);
        }

        public Response Error(GraphQLError error)
        {
            return new Response(error.StatusCode, error.ErrorMessage);
        }

        //public Response AccessDeniedError()
        //{
        //    var error = new AccessDeniedError();
        //    return new Response(error.StatusCode, error.ErrorMessage);
        //}

        //public Response NotFoundError(string id)
        //{
        //    var error = new NotFoundError(id);
        //    return new Response(error.StatusCode, error.ErrorMessage);
        //}
    }
}
