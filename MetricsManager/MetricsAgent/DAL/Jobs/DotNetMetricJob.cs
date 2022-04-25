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
        private PerformanceCounter _dotNetCounter;
        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter
            (
                ".NET CLR Memory",
                "# Bytes in all Heaps",
                "_Global_"
            );
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости CPU
            var dotNetUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Models.DotNetMetric
            {
                Time = time,
                Value =
            dotNetUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
