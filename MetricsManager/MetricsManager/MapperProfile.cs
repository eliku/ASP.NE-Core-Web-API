using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Responses;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, AgentsResponse>();

            CreateMap<CpuMetric, AllCpuMetricsApiResponse>();

            CreateMap<DotNetMetric, AllDotNetMetricsApiResponse>();

            CreateMap<HddMetric, AllHddMetricsApiResponse>();

            CreateMap<NetworkMetric, AllNetworkMetricsApiResponse>();

            CreateMap<RamMetric, AllRamMetricsApiResponse>();
        }
    }
}
