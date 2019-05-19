using System;
using System.Collections.Generic;
using System.Text;

namespace Ingress.DataProviders.LiteDb.Entities
{
    public class Library
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HoursOfOperation { get; set; }
        public bool Cybernavigator { get; set; }
        public bool TeacherInLibrary { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public IEnumerable<int> Visitors { get; set; }
    }
}
