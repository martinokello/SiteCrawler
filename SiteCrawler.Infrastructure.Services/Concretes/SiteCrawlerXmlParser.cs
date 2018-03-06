using SiteCrawler.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using SiteCrawler.Infrastructure.Services.Helpers;

namespace SiteCrawler.Infrastructure.Services.Concretes
{
    public class SiteCrawlerXmlParser : IXmlParser
    {
        private HtmlDocument _htmlContent;
        private string _structureDirectory;
        public SiteCrawlerXmlParser(string domain, string structureDirectory)
        {
            SiteDomain = domain;
            HtmlWeb htmlRequest = new HtmlWeb();
            _htmlContent = htmlRequest.Load(domain);
            _structureDirectory = structureDirectory;
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
                if (!string.IsNullOrEmpty(p))
                {                    
                    if (p.StartsWith(".."))
                    {
                        p = GetPreviousDirectory(_structureDirectory,1) + p;
                    }
                    else if (p.StartsWith("."))
                    {
                        p = GetPreviousDirectory(_structureDirectory, 0) + p;
                    }

                    if (p.ToLower().StartsWith("/"))
                    {
                        p = RootDomain.Substring(0, RootDomain.LastIndexOf("/")) + p;
                    }

                    p = UrlCrawlerHelper.GetAbsoluteUrl(p);

                    if (p.ToLower().StartsWith(RootDomain.Substring(0, RootDomain.LastIndexOf("/"))))
                    {
                        if (p.Contains("?"))
                        {
                            p = p.Substring(0, p.IndexOf("?"));
                        }

                        anchorList.Add(p);
                    }
                }
            }

            return anchorList.Union(new List<string>()).ToList();
        }

        private string GetPreviousDirectory(string structureDirectory, int previousNthDirectory)
        {
            var calculatedDirectory = string.Empty;
            if (structureDirectory.ToLower().Equals(RootDomain.ToLower()))
            {
                return structureDirectory;
            }
            var directories = structureDirectory.Split(new char[] { '/' },StringSplitOptions.RemoveEmptyEntries);

            for(var n = previousNthDirectory; n >= 0 && n < directories.Length; n--)
            {
                calculatedDirectory = structureDirectory.Substring(0, structureDirectory.LastIndexOf(directories[n]))  ;
                if (n == previousNthDirectory - 1) break;
            }
            return calculatedDirectory;
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
