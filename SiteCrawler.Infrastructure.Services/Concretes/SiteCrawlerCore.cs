using SiteCrawler.Domain.Models.Models;
using SiteCrawler.Infrastructure.Services.Interfaces;
using SiteCrawler.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCrawler.Infrastructure.Services.Concretes
{
    public class SiteCrawlerCore : ICrawler
    {
        public SiteCrawlerRepositoryServices RepositoryServices { get; set; }
        private SiteCrawlerUrlProcessor _siteprocessor;
        public string RootDomain{get;set;}
        public SiteCrawlerCore(string url)
        {
            UrlToPagesMapper = new Dictionary<string, string[]>();
            _siteprocessor = new SiteCrawlerUrlProcessor();
        }

        public Dictionary<string, string[]> UrlToPagesMapper{ get;set;}

        public void Crawl(string url)
        {
            var pageLink = _siteprocessor.NormalizeLink(url);

            if (!string.IsNullOrEmpty(pageLink))
            {
                var xmlParser = new SiteCrawlerXmlParser(url, pageLink);
                xmlParser.RootDomain = RootDomain;
                _siteprocessor.RootDomain = RootDomain;
                var localLinks = xmlParser.GetLocalLinks();
                var contentLinks = localLinks.Where(p => _siteprocessor.IsContentPage(p)).ToList();

                if (!UrlToPagesMapper.ContainsKey(pageLink))
                {
                    UrlToPagesMapper.Add(pageLink, contentLinks.ToArray());
                }
                foreach (var childlink in contentLinks)
                {

                    //WriteToDatabase(pageLink, childlink);
                    if (UrlToPagesMapper.ContainsKey(childlink)) continue;
                    var rateOfCrawling = 1000;
                    Int32.TryParse(ConfigurationManager.AppSettings["IntervalOfCrawling"], out rateOfCrawling);

                    Thread.Sleep(10/*rateOfCrawling*/);
                    Crawl(childlink);
                }
            }
        }

        public void WriteToDatabase(string domainUrl, string childlink)
        {

            var site = RepositoryServices.GetSitesByDomain(RootDomain);
            if(site == null)
            {
                site = new Site { LastCrawledDate = DateTime.Now, SiteDomain = RootDomain };
            }
            else
            {
                site.LastCrawledDate = DateTime.Now;
            }
           
            RepositoryServices.SaveSite(site);
            
            RepositoryServices.SaveSitePage(new SitePage { Site = site, SiteId = site.SiteId, PageKey = childlink, PageUrl = childlink, ParentPage = domainUrl });

        }
    }
}
