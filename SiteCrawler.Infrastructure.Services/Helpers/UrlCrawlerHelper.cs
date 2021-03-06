﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Infrastructure.Services.Helpers
{
    public class UrlCrawlerHelper
    {
        public static string RootUrl { get; set; }
        public static string GetAbsoluteUrl(string relativeUrl)
        {
            //Relative url . and .. taken care of while crawling.
            if (!relativeUrl.Contains("./") && !relativeUrl.Contains("../") && !relativeUrl.StartsWith("/"))
            {
                return relativeUrl;
            }
            if (relativeUrl.Contains("./"))
            {
                return GetPartComposedUrl(relativeUrl, "./");
            }

            if (relativeUrl.Contains("../"))
            {
                return GetPartComposedUrl(relativeUrl, "../");
            }

            if(relativeUrl.StartsWith("/"))
            {
                return RootUrl.Trim('/') + relativeUrl;
            }

            return GetAbsoluteUrl(relativeUrl);
        }

        private static string GetPartComposedUrl(string relativeUrl,string relativeSymbol)
        {

            var componentUrl = relativeUrl.Substring(relativeUrl.IndexOf(relativeSymbol) + relativeSymbol.Length);

            var baseUrl = relativeUrl.Substring(0, relativeUrl.IndexOf(relativeSymbol));

            return GetAbsoluteUrl(baseUrl +"/" + componentUrl);
        }
    }
}
