using SiteCrawler.Infrastructure.Services.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCrawler.Infrastructure.Services.Helpers;

namespace SiteCrawler.Infrastructure.Services.Concretes
{
    public class SiteCrawlerUrlProcessor : IUrlProcessor
    {
        public string RootDomain { get; set; }
        public string GetPageFromLink(string url)
        {
            var linkNormalized = StripQueryAndParameters(url);
            var page = GetContentPageName(url);
            return page;

        }

        public string NormalizeLink(string url)
        {
            if (IsContentPage(url))
            {
                return StripQueryAndParameters(url);
            }
            return string.Empty;
        }
        private string StripQueryAndParameters(string url)
        {
            return /*url.Contains("?") ? url.Substring(0, url.LastIndexOf("?")): */url;
        }

        public bool IsContentPage(string url)
        {
            if (!url.ToLower().StartsWith(RootDomain.ToLower())) return false;
            if (Regex.IsMatch(url, ".*\\?prodId=[c|C]heck[a-zA-Z0-9]+$")) return true;
            if (Regex.IsMatch(url.ToLower(), ".*/0+$")) return false;
            var validExtensions = ConfigurationManager.AppSettings["RestrictedHtmlContent"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (url.EndsWith("/"))return true;
            if(url.StartsWith("#"))return false;
            if (Regex.IsMatch(url.ToLower(), ".*/[a-z]+$")) return true;
            if (Regex.IsMatch(url.ToLower(), ".*/[a-z]+$")) return true;
            if (Regex.IsMatch(url.ToLower(), ".*/(\\d+)$")) return true;
            if (url.Contains("."))
            {
                foreach(var ext in validExtensions)
                {
                    if (url.ToLower().EndsWith(ext.ToLower()))return true;
                }
                return false;
            }
            return true;
        }

        public string GetContentPageName(string url)
        {
            url = UrlCrawlerHelper.GetAbsoluteUrl(url);
            if (Regex.IsMatch(url, ".*\\?prodId=[c|C]heck[a-zA-Z0-9]+$")) return url;

            if (IsContentPage(url) && url.Contains("/") && url.ToLower().StartsWith(RootDomain.ToLower().Trim('/')))
            {
                if (url.EndsWith("/")) return url;

                return url.Substring(url.LastIndexOf("/"));
            }
            else if (IsContentPage(url))
            {
                return url;
            }
            else return "";
        }
    }
}
