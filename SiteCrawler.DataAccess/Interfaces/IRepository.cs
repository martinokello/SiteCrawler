using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCarwler.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T[] GetAll();
        bool Add(T item);
        bool Delete(T item);
        bool Update(T item);
    }
}
