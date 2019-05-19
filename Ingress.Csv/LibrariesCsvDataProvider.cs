using Ingress.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ingress.Csv
{
    public class LibrariesCsvDataProvider : IIngressProvider
    {
        private const string LibrariesFilenameSettingKey = "libraries-filename";
        private const string VisitorsFilenameSettingKey = "visitors-filename";
        private readonly LibraryParser libraryParser;
        private readonly VisitorsParser visitorsParser;
        private readonly IDataProvider dataProvider;
        private readonly ILogger<LibrariesCsvDataProvider> logger;

        public LibrariesCsvDataProvider(LibraryParser libraryParser, VisitorsParser visitorsParser, IDataProvider dataProvider, ILogger<LibrariesCsvDataProvider> logger)
        {
            this.libraryParser = libraryParser ?? throw new ArgumentNullException(nameof(libraryParser));
            this.visitorsParser = visitorsParser ?? throw new ArgumentNullException(nameof(visitorsParser));
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public (int, Exception) ProcessData(IDictionary<string, object> settings)
        {
            if (!settings.TryGetValue(LibrariesFilenameSettingKey, out var librariesFilename))
            {
                return (0, new ArgumentException(LibrariesFilenameSettingKey));
            }

            if (!settings.TryGetValue(VisitorsFilenameSettingKey, out var visitorsFilename))
            {
                visitorsFilename = null;
                logger.LogWarning("No visitors data for libraries import");
            }

            try
            {
                var fileLines = File.ReadAllLines((string)librariesFilename);
                IDictionary<string, IEnumerable<int>> visitors = null;

                if (visitorsFilename != null)
                {
                    var visitorFileLines = File.ReadAllLines((string)visitorsFilename);
                    visitors = visitorsParser.ParseVisits(visitorsParser.RemoveHeaders(visitorFileLines));
                }

                var libraries = fileLines
                    .Skip(1)
                    .Select(_ => libraryParser.GetLibrary(_))
                    .Select(_ =>
                    {
                        _.Visitors = visitors.ContainsKey(_.Name) ? visitors[_.Name] : null;
                        return _;
                    });
                dataProvider.SaveData(libraries);
                return (libraries.Count(), null);
            }
            catch (Exception ex)
            {
                return (0, ex);
            }
        }
    }
}
