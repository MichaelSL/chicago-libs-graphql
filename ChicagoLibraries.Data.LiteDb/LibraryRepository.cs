using System;
using System.Collections.Generic;
using System.Text;
using ChicagoLibraries.Data.Model;
using LiteDB;

namespace ChicagoLibraries.Data.LiteDb
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly string connectionString;
        private const string LibrariesTableName = "Libraries";

        public LibraryRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IEnumerable<Library> GetLibraries()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Library>(LibrariesTableName);
                return collection.FindAll();
            }
        }

        public Library GetLibrary(string name)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Library>(LibrariesTableName);
                return collection.FindOne(l => l.Name == name);
            }
        }
    }
}
