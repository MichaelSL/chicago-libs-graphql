using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ingress.Csv
{
    public class VisitorsParser
    {
        private const int FieldsCount = 14;
        private const char StringDelimeter = '"';
        private const char StringSeparator = ',';

        public IDictionary<string, IEnumerable<int>> ParseVisits(IEnumerable<string> dataLines)
        {
            var data = dataLines
                .Select(line => ParseVisit(line))
                .ToDictionary(input => input.Item1, input => input.Item2);
            return data;
        }

        public (string, IEnumerable<int>) ParseVisit(string visit)
        {
            var entries = LinesSplitter.SplitLine(visit, StringDelimeter, StringSeparator);
            if (entries.Count != FieldsCount)
            {
                throw new System.Exception("Visits data should contain library name, 12 values for months and YTD column (14 total)");
            }
            return (entries.First().TrimStart().TrimEnd('*', ' '), entries.Skip(1).Select(_ => int.Parse(_)));
        }

        public IEnumerable<string> RemoveHeaders(IEnumerable<string> dataLines)
        {
            var lines = dataLines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Split(',').Skip(1).All(x => int.TryParse(x, out var _)))
                    return lines.Skip(i);
            }

            throw new System.Exception("No parsable visitors data found");
        }
    }
}
