using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCrawler.Domain.Models.Models;
using SiteCrawlerDBContext.DataAccess.Abstracts;
using SiteCrawler.DataAccess.Interfaces;

namespace SiteCrawler.DataAccess.Concretes
{
    public class SitePageRepository : AbstractRepository<SitePage>, ISitePageRepositoryMarker
    {
        public override SitePage GetById(int id)
        {
            return SiteCrawlerDBContext.SitePages.SingleOrDefault(p => p.PageId == id);
        }

        public override bool Update(SitePage item)
        {
            var entity = SiteCrawlerDBContext.SitePages.SingleOrDefault(p => p.PageId == item.PageId);

            if(entity != null)
            {
                entity.PageKey = item.PageKey;
                entity.SiteId = item.SiteId;
                return true;
            }
            return false;
        }
    }
}
