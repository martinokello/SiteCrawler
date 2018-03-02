using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCarwler.DataAccess.Interfaces;
using SiteCrawler.Domain.Models.Models;
using SiteCrawlerDBContext.DataAccess.Abstracts;
using SiteCrawler.DataAccess.Interfaces;

namespace SiteCrawler.DataAccess.Concretes
{
    public class SiteRepository: AbstractRepository<Site>, ISiteRepositoryMarker
    {
        public override Site GetById(int id)
        {
            return SiteCrawlerDBContext.Sites.SingleOrDefault(p => p.SiteId == id);
        }

        public override bool Update(Site item)
        {
            var entity = SiteCrawlerDBContext.Sites.SingleOrDefault(p => p.SiteId == item.SiteId);
            if (entity != null)
            {
                entity.SiteDomain = item.SiteDomain;
                return true;
            }
            return false;
        }
    }
}
