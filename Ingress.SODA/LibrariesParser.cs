using Ingress.Core.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ingress.SODA
{
    public class LibrariesParser
    {
        public IList<Library> GetLibraries(string json)
        {
            var librariesJson = JArray.Parse(json);

            var libraries = librariesJson
                .Children()
                .Select(l => new Library
                {
                    Name = l["name_"]?.Value<string>(),
                    HoursOfOperation = l["hours_of_operation"]?.Value<string>(),
                    Address = l["address"]?.Value<string>(),
                    City = l["city"]?.Value<string>(),
                    State = l["state"]?.Value<string>(),
                    Zip = l["zip"]?.Value<string>(),
                    Phone = l["phone"]?.Value<string>(),
                    Website = l["website"]?.Value<string>(),
                    Lat = l["location"]["coordinates"][0].Value<decimal>(),
                    Lon = l["location"]["coordinates"][1].Value<decimal>()
                })
                .ToList();

            return libraries;
        }
    }
}
