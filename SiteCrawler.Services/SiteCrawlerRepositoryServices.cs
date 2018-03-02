using SiteCrawler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCrawler.Services.Concretes;
using SiteCrawler.Domain.Models.Models;

namespace SiteCrawler.Services
{
    public class SiteCrawlerRepositoryServices
    {
        SiteCrawlerUnitOfWork _siteCrawlerUnitOfWork;
        public SiteCrawlerRepositoryServices() { }

        public SiteCrawlerRepositoryServices(IUnitOfWork unitOfWork)
        {
            _siteCrawlerUnitOfWork = unitOfWork as SiteCrawlerUnitOfWork;
        }

        //functions for CRUD DB operations Go here

        public void SaveSite(Site site)
        {
            var actualSite = _siteCrawlerUnitOfWork._siteRepository.SiteCrawlerDBContext.Sites.SingleOrDefault(p => p.SiteDomain.ToLower().Equals(site.SiteDomain.ToLower()));
            if (actualSite == null)
            {
                _siteCrawlerUnitOfWork._siteRepository.Add(site);
                _siteCrawlerUnitOfWork.SaveChanges();
            }
        }

        public void SaveSitePage(SitePage sitePage)
        {
                if(sitePage.PageId > 0)
                {
                    var dataPage = _siteCrawlerUnitOfWork._sitePageRepository.GetById(sitePage.PageId);
                    dataPage.PageKey = sitePage.PageKey;
                    dataPage.PageUrl = sitePage.PageUrl;
                }
                else
                {
                    _siteCrawlerUnitOfWork._sitePageRepository.Add(sitePage);
                }
                _siteCrawlerUnitOfWork.SaveChanges();
            
        }

        public void DeleteSitePage(SitePage sitePage)
        {
            var dataPage = _siteCrawlerUnitOfWork._sitePageRepository.GetById(sitePage.PageId);
            if(dataPage != null)
            {
                _siteCrawlerUnitOfWork._sitePageRepository.Delete(dataPage);
                _siteCrawlerUnitOfWork.SaveChanges();

            }

        }

        public void DeleteSite(Site site)
        {
            var dataSite = _siteCrawlerUnitOfWork._siteRepository.GetById(site.SiteId);
            if(dataSite != null)
            {
                var dataPages = _siteCrawlerUnitOfWork._sitePageRepository.GetAll().Where(p => p.SiteId == site.SiteId);
                foreach (var dataPage in dataPages)
                {
                    _siteCrawlerUnitOfWork._sitePageRepository.Delete(dataPage);
                }
                _siteCrawlerUnitOfWork.SaveChanges();
                _siteCrawlerUnitOfWork._siteRepository.Delete(dataSite);
                _siteCrawlerUnitOfWork.SaveChanges();
            }
        }

        public Site GetSitesByDomain(string rootDomain)
        {
            return _siteCrawlerUnitOfWork._siteRepository.SiteCrawlerDBContext.Sites.SingleOrDefault(p => p.SiteDomain.ToLower().Equals(rootDomain.ToLower()));
        }
    }
}
