using ChicagoLibraries.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicagoLibraries.Data
{
    public interface ILibraryRepository
    {
        IEnumerable<Library> GetLibraries();
        Library GetLibrary(string name);
    }
}
