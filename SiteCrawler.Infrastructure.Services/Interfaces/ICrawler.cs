using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Infrastructure.Services.Interfaces
{
    public interface ICrawler
    {
        void Crawl(string url);
        Dictionary<string, string[]> UrlToPagesMapper { get; set; }
    }
}
