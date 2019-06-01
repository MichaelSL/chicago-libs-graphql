using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChicagoLibraries.GraphQLHost
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
