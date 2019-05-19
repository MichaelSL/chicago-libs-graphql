using FluentAssertions;
using Ingress.Core.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ingress.Csv.UnitTests
{
    public class LibraryParserTests
    {
        [Theory]
        [MemberData(nameof(GetLinesTestData))]
        public void GetLibrary_ReturnsLibrary_WhenLineHasCorrectData(string line, Library expected)
        {
            var libParser = new LibraryParser();
            var res = libParser.GetLibrary(line);

            res.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetLinesTestData()
        {
            yield return new object[] { "Blackstone,\"M, W: 12PM-8PM; TU, TH: 10AM-6PM; F, SA: 9AM-5PM; SU: Closed\",Yes,Yes ,4904 S. Lake Park Avenue,CHICAGO,IL,60615,(312) 747-0511,https://www.chipublib.org/locations/12/,\"(41.8058403098503, -87.58934182212592)\"",  new Library
            {
                Name = "Blackstone",
                HoursOfOperation = "M, W: 12PM-8PM; TU, TH: 10AM-6PM; F, SA: 9AM-5PM; SU: Closed",
                Cybernavigator = true,
                TeacherInLibrary = true,
                Address = "4904 S. Lake Park Avenue",
                City = "CHICAGO",
                State = "IL",
                Zip = "60615",
                Phone = "(312) 747-0511",
                Website = "https://www.chipublib.org/locations/12/",
                Lat = 41.8058403098503m,
                Lon = -87.58934182212592m
            }};

            yield return new object[] { "\"Daley, Richard J.-Bridgeport\",\"M, W: 10AM-6PM; TU, TH: 12PM-8PM; F, SA: 9AM-5PM; SU: Closed\",Yes,Yes ,3400 S. Halsted Street,CHICAGO,IL,60608,(312) 747-8990,https://www.chipublib.org/locations/23/,\"(41.83228703346308, -87.64648501124545)\"", new Library
            {
                Name = "Daley, Richard J.-Bridgeport",
                HoursOfOperation = "M, W: 10AM-6PM; TU, TH: 12PM-8PM; F, SA: 9AM-5PM; SU: Closed",
                Cybernavigator = true,
                TeacherInLibrary = true,
                Address = "3400 S. Halsted Street",
                City = "CHICAGO",
                State = "IL",
                Zip = "60608",
                Phone = "(312) 747-8990",
                Website = "https://www.chipublib.org/locations/23/",
                Lat = 41.83228703346308m,
                Lon = -87.64648501124545m
            }};
        }
    }
}
