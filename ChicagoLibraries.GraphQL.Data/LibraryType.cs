using ChicagoLibraries.GraphQL.Data.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicagoLibraries.GraphQL.Data
{
    public class LibraryType: ObjectGraphType<Library>
    {
        public LibraryType()
        {
            Name = "Public Library";
            Description = "Publich library data.";

            Field(l => l.Name).Description("Library name");
            Field(l => l.Address).Description("Library address");

            Field(l => l.Visitors, nullable: true).Description("Visitors by month and cumulative total in last column");
        }
    }
}
