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

            CreateMap<CpuMetric, AllCpuMetricsResponse>();

            CreateMap<DotNetMetric, AllDotNetMetricsResponse>();

            CreateMap<HddMetric, AllHddMetricsResponse>();

            CreateMap<NetworkMetric, AllNetworkMetricsResponse>();

            CreateMap<RamMetric, AllRamMetricsResponse>();
        }
    }
}
