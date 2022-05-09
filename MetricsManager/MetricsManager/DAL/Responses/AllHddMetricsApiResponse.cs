using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Responses
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetric> Metrics { get; set; }
    }
}
