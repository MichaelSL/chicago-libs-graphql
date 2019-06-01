using ChicagoLibraries.Data;
using ChicagoLibraries.Data.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicagoLibraries.GraphQL.Data
{
    public class LibraryQuery: ObjectGraphType<object>
    {
        public LibraryQuery(ILibraryRepository libraryRepository)
        {
            Name = "Query";

            Field<ListGraphType<LibraryType>>("libraries", resolve: context => libraryRepository.GetLibraries());
            Field<LibraryType>("library",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
                {
                    Name = "name",
                    Description = "Library name"
                }),
                resolve: context => libraryRepository.GetLibrary(context.GetArgument<string>("name")));
        }
    }
}
