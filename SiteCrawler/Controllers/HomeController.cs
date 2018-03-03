using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteCrawler.Infrastructure.Services.Concretes;
using SiteCrawler.Caching;
using System.Configuration;
using SiteCrawler.Services;
using SiteCrawler.Domain.Models.Models;
using SiteCrawler.DataAccess.Concretes;

namespace SiteCrawler.Controllers
{
    public class HomeController : Controller
    {
        private SiteCrawlerCore _siteCrawlerEngine;
        private Caching.Caching<Dictionary<string, string[]>> _siteCrawlerCaching;
        private SiteCrawlerRepositoryServices _reporsitoryService;


        public HomeController()
        {
            //Dependency Injection using Unity Is Possible but haven't the time for this exercise.
            _siteCrawlerCaching = new Caching<Dictionary<string, string[]>>();
            var unitOfWork = new Services.Concretes.SiteCrawlerUnitOfWork(new SiteRepository(), new SitePageRepository());
            _reporsitoryService = new SiteCrawlerRepositoryServices(unitOfWork);
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.TimeElapsed = "";
            return View("Index",new Dictionary<string,string[]>());
        }
        [HttpPost]
        public ActionResult Index(string domainUrl)
        {
            ModelState.Clear();

            ViewBag.Title = "Home Page";
            if (!IsValidDomain(domainUrl))
            {
                ModelState.AddModelError("domainUrl", "Domain is of wrong format, please enter correct domain url");
                return View(new Dictionary<string, string[]>());
            }
            try
            {
                if(!domainUrl.EndsWith("/"))domainUrl += "/";
                _siteCrawlerEngine = new SiteCrawlerCore(domainUrl);
                _siteCrawlerEngine.RootDomain = domainUrl;

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //Thought of Caching but not required due to high memory usage: therefore DB persistence required.
                ///////////////////////////////////////////////////////////////////////////////////////////////////

                //Func<Dictionary<string, string[]>> getOrSaveInCache = () => { _siteCrawlerEngine.Crawl(domainUrl); return _siteCrawlerEngine.UrlToPagesMapper; };

                //var cachedSiteMap =_siteCrawlerCaching.GetFromCache(string.Format("SiteResults{0}", domainUrl),Int32.Parse(ConfigurationManager.AppSettings["SiteResultDaysToCache"])*3600*24, getOrSaveInCache);
                _siteCrawlerEngine.RepositoryServices = _reporsitoryService;
                var startTime =DateTime.Now;
                _siteCrawlerEngine.Crawl(domainUrl);
                var endTime = DateTime.Now;
                var timeSpan = endTime - startTime;

                ViewBag.TimeElapsed = string.Format("Duration of Site Crawl: {0} hours {1} minutes {2} seconds",
                    timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                 //var cachedSiteMap = _siteCrawlerEngine.UrlToPagesMapper;
                return View("Index", _siteCrawlerEngine.UrlToPagesMapper);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("domainUrl", ex.Message);
                return View(new Dictionary<string, string[]>());
            }
        }

        private bool IsValidDomain(string domain)
        {
            if (string.IsNullOrEmpty(domain)) return false;
            var domainPattern = "^(http:|https:|)[/][/]([^/]+[.])*[a-zA-Z]+[(.com)|(.co.uk)]$";
            return Regex.IsMatch(domain.ToLower(), domainPattern); 
        }

    }
}
