using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MPKXMLCrawler
{
    [NodeRequest(requestType = Requests.Types.Lines)]
    [NodeXPath(path = "/html/body/table/tbody/tr/td/table/tr[2]/td/table/tr/td/details/table/tr")]
    public class LineClass
    {
        [NodeAttribute(name = "class", xpath = ".//*[@{0}]")]
        public string lineClass { get; set; }

        [NodeInnerText]
        public string description { get; set; }

    }
}
