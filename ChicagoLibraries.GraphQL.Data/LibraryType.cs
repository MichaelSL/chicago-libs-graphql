using ChicagoLibraries.Data.Model;
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
            Name = "Library";
            Description = "Publich library data.";

            Field(l => l.Name).Description("Library name");
            Field(l => l.Address).Description("Library address");
            Field(l => l.City).Description("Library city");
            Field(l => l.State).Description("Library state");
            Field(l => l.Zip).Description("ZIP code");
            Field(l => l.Phone, nullable: true).Description("Library phone number");
            Field(l => l.Website, nullable: true).Description("Library website");
            Field(l => l.HoursOfOperation).Description("Library working hours");
            Field(l => l.Cybernavigator).Description("Cybernavigator is available in library");
            Field(l => l.TeacherInLibrary).Description("Teacher is available in library");
            Field(l => l.Lat).Description("Library latitude");
            Field(l => l.Lon).Description("Library longitude");

            Field(l => l.Visitors, nullable: true).Description("Visitors by month and cumulative total in last column");
        }
    }
}