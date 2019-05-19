using Ingress.Core;
using Ingress.Core.Model;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ingress.DataProviders.LiteDb
{
    public class LibrariesLiteDbDataProvider : IDataProvider
    {
        private readonly LiteDbRepository liteDbRepository;

        public LibrariesLiteDbDataProvider(LiteDbRepository liteDbRepository)
        {
            this.liteDbRepository = liteDbRepository ?? throw new ArgumentNullException(nameof(liteDbRepository));
        }

        public void SaveData(IEnumerable<Library> libraries)
        {
            var libraryEntities = libraries.Adapt<IEnumerable<Entities.Library>>();

            liteDbRepository.SaveData<Entities.Library>(libraryEntities, "Libraries");
        }
    }
}
