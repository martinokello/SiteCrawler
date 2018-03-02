using System.Linq;
using SiteCarwler.DataAccess.Interfaces;

namespace SiteCrawlerDBContext.DataAccess.Abstracts
{
    public abstract class AbstractRepository<T> : IRepository<T>  where T : class
    {
        public SiteCrawler.DataAccess.SiteCrawlerDBContext SiteCrawlerDBContext { get; set; }
        public bool Add(T item)
        {
            try
            {
                SiteCrawlerDBContext.Set<T>().Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(T item)
        {
            try
            {
                SiteCrawlerDBContext.Set<T>().Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T[] GetAll()
        {
            try
            {
                return SiteCrawlerDBContext.Set<T>().ToArray<T>();
            }
            catch
            {
                return null;
            }
        }

        public abstract T GetById(int id);


        public abstract bool Update(T item);
    }
}
