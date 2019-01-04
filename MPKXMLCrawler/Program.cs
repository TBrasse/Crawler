using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;


namespace MPKXMLCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestDispatcher.SetMobileApp();
            //HtmlDocument document = MPKRequest.GetRawSchedule("PL", "20181227", "11");
            //HtmlNode node = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/table[1]/thead[1]/tr[3]/td[1]/table[1]/tr[1]/td[1]");
            //HtmlDocument document = RequestDispatcher.GetLineIndex("PL", "index", "20181227");
            //HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(XpathLibrary.LineClass);           
            HTMLScraper scraper = new HTMLScraper();
            //List<LineClass> lineClasses = scraper.Scrap<LineClass>();
            List<Line> listLine = scraper.Scrap<Line>();
        }
    }
}
