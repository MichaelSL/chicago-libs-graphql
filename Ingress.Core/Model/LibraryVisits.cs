using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingress.Core.Model
{
    public class LibraryVisits
    {
        public string LibraryName { get; set; }
        public IEnumerable<int> Visits { get; set; }
        public int Total { get; set; }
        public IEnumerable<int> AllVisits => Visits.Concat(new[] { Total });
    }
}
