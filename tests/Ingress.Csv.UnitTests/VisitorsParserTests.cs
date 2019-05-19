using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ingress.Csv.UnitTests
{
    public class VisitorsParserTests
    {
        [Theory]
        [MemberData(nameof(GetVisitorLinesTestData))]
        public void ParseVisit_ShouldReturnValidData(string line, string expectedName, IEnumerable<int> expectedVisits)
        {
            var parser = new VisitorsParser();
            var (name, visits) = parser.ParseVisit(line);

            name.Should().BeEquivalentTo(expectedName);
            visits.Should().BeEquivalentTo(expectedVisits);
        }

        public static IEnumerable<object[]> GetVisitorLinesTestData()
        {
            yield return new object[] { "Albany Park,15687,10569,13383,12505,12000,12966,12637,12890,12309,14294,11525,10795,151560", "Albany Park", new [] { 15687, 10569, 13383, 12505, 12000, 12966, 12637, 12890, 12309, 14294, 11525, 10795, 151560 } };
            yield return new object[] { "Archer Heights*,7060,6469,9119,7224,7091,7566,7774,7905,8624,8514,7341,6807,91494", "Archer Heights", new [] { 7060, 6469, 9119, 7224, 7091, 7566, 7774, 7905, 8624, 8514, 7341, 6807, 91494 } };
            yield return new object[] { "\"Daley, Richard M.- W Humboldt * \",9969,9543,11811,11134,10889,10850,12497,12122,10557,12398,10288,9863,131921", "Daley, Richard M.- W Humboldt", new [] { 9969, 9543, 11811, 11134, 10889, 10850, 12497, 12122, 10557, 12398, 10288, 9863, 131921 } };
        }
    }
}
