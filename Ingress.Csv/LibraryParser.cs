using Ingress.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ingress.Csv
{
    public class LibraryParser
    {
        private const int FieldsCount = 11;
        private const char StringDelimeter = '"';

        public Library GetLibrary(string line, char separator = ',')
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            var lineValues = LinesSplitter.SplitLine(line, StringDelimeter, separator);

            if (lineValues.Count != FieldsCount)
            {
                throw new InvalidOperationException($"Parser supports only {FieldsCount} fields data sets.");
            }

            return CreateLibrary(lineValues);
        }

        private Library CreateLibrary(IList<string> lineValues)
        {
            var lib = new Library();

            lib.Name = lineValues[0];
            lib.HoursOfOperation = lineValues[1];
            lib.Cybernavigator = FromYesNo(lineValues[2]) ?? throw new ArgumentOutOfRangeException("Cybernavigator");
            lib.TeacherInLibrary = FromYesNo(lineValues[3]) ?? throw new ArgumentOutOfRangeException("Teacher In The Library");
            lib.Address = lineValues[4];
            lib.City = lineValues[5];
            lib.State = lineValues[6];
            lib.Zip = lineValues[7];
            lib.Phone = lineValues[8];
            lib.Website = lineValues[9];
            (lib.Lat, lib.Lon) = ParseLocation(lineValues[10]);

            return lib;
        }

        public bool? FromYesNo(string input)
        {
            input = input.Trim().ToLowerInvariant();
            return input == "yes" ? true : input == "no" ? (bool?)false : null;
        }

        public (decimal, decimal) ParseLocation(string input)
        {
            var latlon = input
                .Trim('"', ')', '(')
                .Split(',')
                .Select(_ => _.Trim(' '))
                .Select(_ => decimal.Parse(_))
                .ToArray();
            return (latlon[0], latlon[1]);
        }
    }
}
