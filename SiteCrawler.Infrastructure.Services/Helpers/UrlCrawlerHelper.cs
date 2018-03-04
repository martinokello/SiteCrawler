using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Infrastructure.Services.Helpers
{
    public class UrlCrawlerHelper
    {
        public static string GetAbsoluteUrl(string relativeUrl)
        {
            //Relative url . and .. taken care of while crawling.
            if (!relativeUrl.Contains("./") && !relativeUrl.Contains("../"))
            {
                return relativeUrl;
            }
            if (relativeUrl.Contains("./"))
            {
                return GetParComposedUrl(relativeUrl, "./");
            }

            if (relativeUrl.Contains("../"))
            {
                return GetParComposedUrl(relativeUrl, "../");
            }

            return GetAbsoluteUrl(relativeUrl);
        }

        private static string GetParComposedUrl(string relativeUrl,string relativeSymbol)
        {

            var componentUrl = relativeUrl.Substring(relativeUrl.IndexOf(relativeSymbol) + relativeSymbol.Length);

            var baseUrl = relativeUrl.Substring(0, relativeUrl.IndexOf(relativeSymbol));

            return GetAbsoluteUrl(baseUrl +"/" + componentUrl);
        }
    }
}
