using SiteCrawler.DataAccess.Concretes;
using SiteCrawler.DataAccess.Interfaces;
using SiteCrawler.Services.Interfaces;

namespace SiteCrawler.Services.Concretes
{
    public class SiteCrawlerUnitOfWork : IUnitOfWork
    {
        public SiteRepository _siteRepository;
        public SitePageRepository _sitePageRepository;


        public DataAccess.SiteCrawlerDBContext SiteCrawlerDbContext { get; set; }
        public SiteCrawlerUnitOfWork() {
            SiteCrawlerDbContext = new DataAccess.SiteCrawlerDBContext();
        }

        public SiteCrawlerUnitOfWork(ISiteRepositoryMarker siteRepositoryMarker,
             ISitePageRepositoryMarker sitePageRepositoryMarker):this()
        {
            _siteRepository = siteRepositoryMarker as SiteRepository;
            _sitePageRepository = sitePageRepositoryMarker as SitePageRepository;
            InitializeDbContext();
        }

        public void InitializeDbContext()
        {
            _siteRepository.SiteCrawlerDBContext = SiteCrawlerDbContext;
            _sitePageRepository.SiteCrawlerDBContext = SiteCrawlerDbContext;
        }
        public void SaveChanges()
        {
            SiteCrawlerDbContext.SaveChanges();
        }


    }
}
