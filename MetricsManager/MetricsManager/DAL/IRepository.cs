using System;
using System.Collections.Generic;

namespace MetricsManager.DAL
{
    public interface IRepository<T> where T:class
    {
        void Create(T item);
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
        IList<T> GetByTimePeriod(int agentId, TimeSpan fromTime, TimeSpan toTime);

        TimeSpan GetAgentLastMetricDate(int agentId);
    }
}
