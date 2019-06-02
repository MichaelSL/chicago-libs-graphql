using Ingress.Core.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ingress.SODA
{
    public class VisitorsParser
    {
        public IList<LibraryVisits> GetLibraryVisits(string json)
        {
            var visitsJson = JArray.Parse(json);

            var visits = visitsJson
                .Children()
                .Select(l => new LibraryVisits
                {
                    LibraryName = l["location"]?.Value<string>()?.Trim(' ', '*'),
                    Visits = new int[]
                    {
                        l["january"].Value<int>(),
                        l["february"].Value<int>(),
                        l["march"].Value<int>(),
                        l["april"].Value<int>(),
                        l["may"].Value<int>(),
                        l["june"].Value<int>(),
                        l["july"].Value<int>(),
                        l["august"].Value<int>(),
                        l["september"].Value<int>(),
                        l["october"].Value<int>(),
                        l["november"].Value<int>(),
                        l["december"].Value<int>()
                    },
                    Total = l["ytd"]?.Value<int>() ?? 0
                })
                .ToList();

            return visits;
        }
    }
}
