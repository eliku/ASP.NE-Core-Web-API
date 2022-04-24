using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IRepository<T> where T:class
    {
        void Create(T item);
        /*
        IList<T> GetAll();
        T GetById(int id);
        
        void Update(T item);
        void Delete(int id);
        */
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
    }
}
