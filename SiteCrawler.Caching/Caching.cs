using System;
using System.Web;
using System.Web.Caching;
using SiteCrawler.Caching.Interfaces;

namespace SiteCrawler.Caching
{
    public class Caching<T> : ICaching<T> where T : class
    {
        public Cache Cache { get { return HttpRuntime.Cache; }  }

        public T GetFromCache(string key, int secondsToCache, Func<T> getRefreshCache)
        {
            var result = (T)HttpRuntime.Cache[key];
            if (result != default(T)) return result;
            result = getRefreshCache();
            HttpRuntime.Cache.Add(key, result, null, DateTime.Now.AddSeconds(secondsToCache), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            return result;
        }
    }
}
