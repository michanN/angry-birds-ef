using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngryBirds.API.Errors
{
    public class GraphQLError
    {
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        protected GraphQLError(string statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
