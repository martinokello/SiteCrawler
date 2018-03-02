using SiteCrawler.Infrastructure.Services.Interfaces;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SiteCrawler.Infrastructure.Services.Concretes
{
    public class SiteCrawlPageRequest : IHttpRequest
    {

        public string GetPageContent(string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                var webRespone = httpWebRequest.GetResponse();
                using (var responseStream = webRespone.GetResponseStream())
                {
                    var streamReader = new StreamReader(responseStream);
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                throw;
            }
            return string.Empty;
        }
    }
}
