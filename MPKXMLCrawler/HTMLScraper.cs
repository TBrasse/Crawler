using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Reflection;

namespace MPKXMLCrawler
{
    public class HTMLScraper
    {
        private HtmlDocument document;

        public List<T> Scrap<T>()
        {
            ActOnAttribute<T,NodeRequestAttribute, HtmlDocument>( s =>{ return null; });

            Type typeToScrap = typeof(T);
            NodeRequestAttribute nrAtrb = typeToScrap.GetCustomAttributes(typeof(NodeRequestAttribute),false).First() as NodeRequestAttribute;
            Requests.Types requestType = nrAtrb.request;
            var request = Requests.GetRequest(requestType);
            document = RequestDispatcher.SendRequest(request);

            NodeXPathAttribute xpAtrb = typeToScrap.GetCustomAttributes(typeof(NodeXPathAttribute), false).First() as NodeXPathAttribute;
            HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes(xpAtrb.path);
            List<T> objectList = new List<T>();
            foreach (HtmlNode node in htmlNodes)
            {
                T scrapObject = (T)Activator.CreateInstance(typeToScrap);
                PropertyInfo[] properties = typeToScrap.GetProperties();
                foreach(PropertyInfo property in properties)
                {
                    NodeAttribute atrb = property.GetCustomAttributes().FirstOrDefault() as NodeAttribute;
                    if (atrb != null)
                    {
                        string objectValue = atrb.Resolve(node);
                        property.SetValue(scrapObject, objectValue);
                    }
                }
                objectList.Add(scrapObject);
            }
            return objectList;
        }

        private TOut ActOnAttribute<T1,T2,TOut>(Func<T2,TOut> action) where T2 : Attribute
        {
            Type typeToScrap = typeof(T1);
            T2 attribute = typeToScrap.GetCustomAttribute<T2>();
            return action.Invoke(attribute);
        }
    }
}
