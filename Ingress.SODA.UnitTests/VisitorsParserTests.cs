using Ingress.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ingress.SODA.UnitTests
{
    public class VisitorsParserTests
    {
        [Fact]
        public void GetLibraryVisits_ReturnData_WhenCorrectInput()
        {
            string input = "[{\"location\":\"Albany Park\",\"january\":\"15687\",\"february\":\"10569\",\"march\":\"13383\",\"april\":\"12505\",\"may\":\"12000\",\"june\":\"12966\",\"july\":\"12637\",\"august\":\"12890\",\"september\":\"12309\",\"october\":\"14294\",\"november\":\"11525\",\"december\":\"10795\",\"ytd\":\"151560\"}\r\n,{\"location\":\"Altgeld\",\"january\":\"2734\",\"february\":\"2575\",\"march\":\"2847\",\"april\":\"2660\",\"may\":\"2653\",\"june\":\"3295\",\"july\":\"2791\",\"august\":\"2840\",\"september\":\"2656\",\"october\":\"2928\",\"november\":\"2380\",\"december\":\"2404\",\"ytd\":\"32763\"}]";
            var visitsParser = new VisitorsParser();
            var expected = new List<LibraryVisits>
            {
                new LibraryVisits
                {
                    LibraryName = "Albany Park"
                }
            };

            var actual = visitsParser.GetLibraryVisits(input);

            Assert.Equal(expected, actual);
        }
    }
}
