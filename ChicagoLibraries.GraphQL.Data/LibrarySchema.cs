using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicagoLibraries.GraphQL.Data
{
    public class LibrarySchema: Schema
    {
        public LibrarySchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<LibraryQuery>();
        }
    }
}
