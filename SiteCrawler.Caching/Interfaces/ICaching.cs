using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCrawler.Caching.Interfaces
{
    public interface ICaching<T> where T : class
    {
        T GetFromCache(string key, int secondsToCache, Func<T> getRefreshCachee);
    }
}
