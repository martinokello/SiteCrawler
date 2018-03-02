using SiteCrawler.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace SiteCrawler.Infrastructure.Services.Concretes
{
    public class SiteCrawlerXmlParser : IXmlParser
    {
        private HtmlDocument _htmlContent;
        public SiteCrawlerXmlParser(string domain)
        {
            SiteDomain = domain;
            HtmlWeb htmlRequest = new HtmlWeb();
            _htmlContent = htmlRequest.Load(domain);
        }
        public string RootDomain { get; set; }
        public string SiteDomain{get;set;}
        public HtmlDocument HtmlContent { get; set; }
        public List<string> GetLocalLinks()
        {
            var anchorList = new List<string>();

           var anchors = _htmlContent.CreateNavigator().SelectDescendants("a", "", false);

            while (anchors.MoveNext())
            {
                var p = anchors.Current.GetAttribute("href", "");
                if (!string.IsNullOrEmpty(p) && p!="/" && (p.ToLower().StartsWith(SiteDomain.ToLower()) || p.ToLower().StartsWith("/") || p.ToLower().StartsWith(".")))
                {
                    if (p.Contains("?")) p = p.Substring(0, p.IndexOf("?"));

                    if (p.ToLower().StartsWith("/")) {
                        p = RootDomain.Substring(0,RootDomain.LastIndexOf("/")) + p;
                    }
                    if (p.ToLower().StartsWith("."))
                    {
                        p = RootDomain.Substring(0, RootDomain.LastIndexOf("/")) + p.Substring(1);
                    }
                    anchorList.Add(p);
                }
            }

            return anchorList.Union(new List<string>()).ToList();
        }

        public XDocument Parse(string xmlContent)
        {
            /*
            if (!string.IsNullOrEmpty(xmlContent))
            {
                _xmlContent = XDocument.Parse(xmlContent);

                return _xmlContent;
            }
            else return new XDocument();
            */
            return null;
        }
    }
}
