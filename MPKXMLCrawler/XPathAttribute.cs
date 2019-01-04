﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MPKXMLCrawler
{
    public class NodeXPathAttribute : Attribute
    {
        public string path;
        public HtmlNodeCollection Resolve(HtmlDocument document)
        {
            return null;
        }
    }

    public class NodeRequestAttribute : Attribute
    {
        public Requests.Types request;
        public HtmlDocument Resolve()
        {
            return null;
        }
    }

    public class NodeAttributeAttribute : NodeAttribute
    {
        public string name;
        public string xpath = "attribute::*";
        public override string Resolve(HtmlNode node) => node.SelectSingleNode(string.Format(xpath, name)).GetAttributeValue(name, null);        
    }

    public class NodeInnerTextAttribute : NodeAttribute
    {
        public string xpath = "self::*";
        public override string Resolve(HtmlNode node) => node.SelectSingleNode(xpath).InnerText.Trim();
    }

    public abstract class NodeAttribute : Attribute
    {
        public abstract string Resolve(HtmlNode node);
    }
}
