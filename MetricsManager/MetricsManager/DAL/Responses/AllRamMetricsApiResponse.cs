using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Responses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetric> Metrics { get; set; }
    }

}
