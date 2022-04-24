using System;
using MetricsAgent.DAL.Repository;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Jobs
{
    public class DotNetMetricJob
    {
        private IDotNetMetricsRepository _repository;
        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Теперь можно записать что-то посредством репозитория
            return Task.CompletedTask;
        }
    }
}
