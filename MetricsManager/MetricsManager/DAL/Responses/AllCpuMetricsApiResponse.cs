using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }


}
