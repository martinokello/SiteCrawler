using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Domain.Models.Models
{
    public class SitePage
    {
        [Key]
        public int PageId { get; set; }
        public string PageKey { get; set; }
        public string ParentPage { get; set; }
        public string PageUrl { get; set; }
        [ForeignKey("Site")]
        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
}
