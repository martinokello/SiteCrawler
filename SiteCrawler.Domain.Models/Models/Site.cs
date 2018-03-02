using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Domain.Models.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public string SiteDomain { get; set; }

        public DateTime LastCrawledDate { get; set; }

    }
}
