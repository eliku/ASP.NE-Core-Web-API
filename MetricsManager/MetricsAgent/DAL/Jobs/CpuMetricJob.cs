using System;
using MetricsAgent.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace MetricsAgent.DAL.Jobs
{
    public class CpuMetricJob:IJob
    {
        private  ICpuMetricsRepository _repository;
        // Счётчик для метрики CPU
        private EventCounter _cpuCounter;

        public CpuMetricJob(ICpuMetricsRepository repository)
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
