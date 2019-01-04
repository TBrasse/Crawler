using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPKXMLCrawler
{
    [NodeRequest(requestType = Requests.Types.Lines)]
    [NodeXPath(path = "/html/body/table/tbody/tr/td/table/tr[1]/td/table/tr/td/a")]
    public class Line
    {
        [NodeInnerText]
        public string Number { get; set; }

        [NodeAttribute(name = "class")]
        public string Class { get; set; }

        [NodeInnerText(xpath = "ancestor::node()[2]/preceding-sibling::tr[1]")]
        public string Group { get; set; }
    }
}
