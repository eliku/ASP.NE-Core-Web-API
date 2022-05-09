using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;


namespace MetricsManager.DAL.Responses
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetric> Metrics { get; set; }
    }
}
