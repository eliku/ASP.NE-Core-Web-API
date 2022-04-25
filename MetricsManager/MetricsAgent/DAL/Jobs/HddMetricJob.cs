using System;
using MetricsAgent.DAL.Repository;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Jobs
{
    public class HddMetricJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;
        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter
            (
                "PhysicalDisk",
                "% Disk Time",
                "_Total"
            );
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости Hdd
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            //узнаем когда сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //теперь можно записать что-то при помощи репозитория
            _repository.Create(new Models.HddMetric
            {
                Time = time,
                Value =
            hddUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
