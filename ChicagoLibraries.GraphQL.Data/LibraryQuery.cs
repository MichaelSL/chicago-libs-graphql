using ChicagoLibraries.GraphQL.Data.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicagoLibraries.GraphQL.Data
{
    public class LibraryQuery: ObjectGraphType<object>
    {
        public LibraryQuery()
        {
            Name = "Query";

            Field<ListGraphType<LibraryType>>("libraries", resolve: context =>
            {
                return new Library[]
                {
                    new Library
                    {
                        Name = "lib1",
                        Address = "street"
                    },
                    new Library
                    {
                        Name = "lib2",
                        Address = " another street"
                    }
                };
            });
        }
    }
}
