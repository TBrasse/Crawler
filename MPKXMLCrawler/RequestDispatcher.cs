using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace MPKXMLCrawler
{
    public class RequestDispatcher
    {
        static string MPKUrl = "rozklady.mpk.krakow.pl";
        private static CookieCollection cookies;

        public static HtmlDocument SendRequest((string,string)[] arguments)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(MakeRequest(arguments));
            return htmlDocument;
        }

        public static HtmlDocument GetRawSchedule(string lang, string date, string line)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(MakeRequest(new[] {
                ("lang", lang),
                ("rozklad", date),
                ("linia", line)
            }));
            return htmlDocument;
        }
        public static HtmlDocument GetLineIndex(string lang, string action, string date)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            string htmlResponse = MakeRequest(new[] {
                ("lang", lang),
                ("akcja", action),
                ("rozklad", date)
            });
            htmlDocument.LoadHtml(htmlResponse);
            return htmlDocument;
        }
        public static void SetMobileApp()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            MakeRequest(new[] {
                ("akcja", "telefon")
            });
            //return htmlDocument;
        }

        private static string MakeRequest((string,string)[] queryParams)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = MPKUrl;
            uriBuilder.Query = GetQuery(queryParams);
            HttpWebRequest request = WebRequest.CreateHttp(uriBuilder.Uri);
            request.CookieContainer = GetCookies(cookies);
            //request.Headers.Add("User-Agent", "Googlebot/2.1 (+http://www.googlebot.com/bot.html)");
            //request.Headers.Add("Content-Type", "application / x - www - form - urlencoded");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            cookies = response.Cookies;
            string responseMessage = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseMessage;
        }    

        private static CookieContainer GetCookies(CookieCollection cookies)
        {
            var cookiesContainer = new CookieContainer();
            if (cookies != null)
            foreach(Cookie cookie in cookies)
            {
                cookiesContainer.Add(cookie);
            }
            return cookiesContainer;
        }

        private static string GetQuery((string,string)[] queryParts)
        {
            StringBuilder queryBuilder = new StringBuilder("?");
            queryParts.ForEach(p => queryBuilder.Append($"{p.Item1}={p.Item2}&"));
            return queryBuilder.ToString();
        }
    }
}
