using System;
using MetricsAgent.DAL.Repository;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Jobs
{
    public class RamMetricJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter; 
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости CPU
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Models.RamMetric
            {
                Time = time,
                Value =
            ramUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
