using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}
