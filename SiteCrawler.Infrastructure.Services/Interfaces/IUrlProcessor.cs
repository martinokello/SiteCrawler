using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Infrastructure.Services.Interfaces
{
    public interface IUrlProcessor
    {
        string GetPageFromLink(string url);
        string NormalizeLink(string url);
    }
}
