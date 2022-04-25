using System;
using MetricsAgent.DAL.Repository;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Jobs
{
    public class NetworkMetricJob
    {
        private  INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;
        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter
            (
                "Network Interface",
                "Bytes Total/sec",
                "Broadcom 802.11ac Network Adapter"
            );
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости CPU
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new Models.NetworkMetric
            {
                Time = time,
                Value =
            networkUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
