
using MetricsManager.DAL.Request;
using MetricsManager.DAL.Responses;

namespace MetricsManager
{
    interface IMetricsAgentClient
    {
        AllRamMetricsApiResponse GetAllRamMetrics(RamMetricRequest request);
        AllNetworkMetricsApiResponse GetAllHddMetrics(NetworkMetricRequest request);
        AllHddMetricsApiResponse GetAllHddMetrics(HddMetricRequest request);
        AllDotNetMetricsApiResponse GetAllDonNetMetrics(DotNetMetricRequest request);
        AllCpuMetricsApiResponse GetAllCpuMetrics(CpuMetricRequest request);
    }
}
