using Ingress.Core.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ingress.SODA.UnitTests
{
    public class LibrariesParserTests
    {
        [Fact]
        public void GetLibraries_ReturnData_WhenCorrectInput()
        {
            string input = "[{\"name_\":\"Vodak-East Side\",\"hours_of_operation\":\"Sun., Closed; Mon. & Wed., Noon-8; Tue. & Thu., 10-6; Fri. & Sat., 9-5\",\"address\":\"3710 E. 106th St.\",\"city\":\"Chicago\",\"state\":\"IL\",\"zip\":\"60617\",\"phone\":\"(312) 747-5281\",\"website\":\"https://www.chipublib.org/locations/71/\",\"location\":{\"type\":\"Point\",\"coordinates\":[-87.53350318470449,41.70302747819179]},\":@computed_region_rpca_8um6\":\"25\",\":@computed_region_vrxf_vc4k\":\"49\",\":@computed_region_6mkv_f3dw\":\"21202\",\":@computed_region_bdys_3d7i\":\"715\",\":@computed_region_43wa_7qmu\":\"47\"}\r\n,{\"name_\":\"South Shore\",\"address\":\"2505 E. 73rd St.\",\"city\":\"Chicago\",\"state\":\"IL\",\"zip\":\"60649\",\"phone\":\"Closed for Construction\",\"website\":\"https://www.chipublib.org/locations/66/\",\"location\":{\"type\":\"Point\",\"coordinates\":[-87.56208363879564,41.76155535585864]},\":@computed_region_rpca_8um6\":\"24\",\":@computed_region_vrxf_vc4k\":\"39\",\":@computed_region_6mkv_f3dw\":\"22538\",\":@computed_region_bdys_3d7i\":\"421\",\":@computed_region_43wa_7qmu\":\"37\"}\r\n,{\"name_\":\"Austin-Irving\",\"hours_of_operation\":\"Sun., Closed; Mon. & Wed., Noon-8; Tue. & Thu., 10-6; Fri. & Sat., 9-5\",\"address\":\"6100 W. Irving Park Rd.\",\"city\":\"Chicago\",\"state\":\"IL\",\"zip\":\"60634\",\"phone\":\"(312) 744-6222\",\"website\":\"https://www.chipublib.org/locations/7/\",\"location\":{\"type\":\"Point\",\"coordinates\":[-87.77938682073541,41.95317390064158]},\":@computed_region_rpca_8um6\":\"52\",\":@computed_region_vrxf_vc4k\":\"15\",\":@computed_region_6mkv_f3dw\":\"22254\",\":@computed_region_bdys_3d7i\":\"131\",\":@computed_region_43wa_7qmu\":\"19\"}]";
            var libsParser = new LibrariesParser();
            var expected = new List<Library>
            {
                new Library
                {
                    Name = "Vodak-East Side"
                }
            };

            var actual = libsParser.GetLibraries(input);

            Assert.Equal(expected, actual);
        }
    }
}
