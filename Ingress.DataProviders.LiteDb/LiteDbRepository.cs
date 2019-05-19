using LiteDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ingress.DataProviders.LiteDb
{
    public class LiteDbRepository
    {
        private const string DatabaseFilenameKey = "DataProviders:LiteDb:database";
        private readonly IConfiguration configuration;

        public LiteDbRepository(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void SaveData<T> (IEnumerable<T> data, string tableName)
        {
            var databaseName = configuration[DatabaseFilenameKey];
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException($"{databaseName} configuration is missing");
            }

            using (var db = new LiteDatabase(databaseName))
            {
                var collection = db.GetCollection<T>(tableName);
                collection.InsertBulk(data);
            }
        }
    }
}
