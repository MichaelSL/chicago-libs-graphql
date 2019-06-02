using Ingress.Core;
using Ingress.Core.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Ingress.SODA
{
    public class LibrariesSODADataProvider : IIngressProvider
    {
        private readonly IDataProvider dataProvider;

        public LibrariesSODADataProvider(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public (int, Exception) ProcessData(IDictionary<string, object> settings)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<(int, Exception)> ProcessDataAsync(IDictionary<string, object> settings)
        {
            string librariesUrl = @"https://data.cityofchicago.org/resource/psqp-6rmg.json";
            string visitorsUrl = @"https://data.cityofchicago.org/resource/i7zz-iiza.json";

            try
            {
                HttpClient httpClient = new HttpClient();

                var librariesString = await httpClient.GetStringAsync(librariesUrl);
                var visitorsString = await httpClient.GetStringAsync(visitorsUrl);

                httpClient.Dispose();

                var libsParser = new LibrariesParser();
                var libraries = libsParser.GetLibraries(librariesString);

                var visitorsParser = new VisitorsParser();
                var visits = visitorsParser.GetLibraryVisits(visitorsString);

                libraries.ToList().ForEach(l =>
                {
                    l.Visitors = visits.FirstOrDefault(v => v.LibraryName == l.Name)?.AllVisits;
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
