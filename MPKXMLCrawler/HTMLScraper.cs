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
        public IEnumerable<T> Scrap<T>()
        {
            Type typeToScrap = typeof(T);
            //add error handling: >1 attrib; =0 attrib 
            NodeRequestAttribute nrAtrb = GetAttribute<NodeRequestAttribute>(typeToScrap);
            HtmlDocument document = nrAtrb.Resolve();

            NodeXPathAttribute xpAtrb = GetAttribute<NodeXPathAttribute>(typeToScrap);
            return xpAtrb.Resolve(document).ForEach(node => {
                //make this smaller somehow:
                T scrapObject = (T)Activator.CreateInstance(typeToScrap);
                foreach (PropertyInfo property in typeToScrap.GetProperties())
                {
                    NodeAttribute atrb = GetAttribute<NodeAttribute>(property);
                    if (atrb != null)
                    {
                        string objectValue = atrb.Resolve(node);
                        property.SetValue(scrapObject, objectValue);
                    }
                }
                return scrapObject;

            });
        }

        private T2 GetAttribute<T2>(MemberInfo typeToScrap) where T2 : Attribute
        {
            return typeToScrap.GetCustomAttribute<T2>();
        }
    }
}
