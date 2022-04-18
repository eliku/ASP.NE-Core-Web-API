using System;


namespace MetricsAgent.DAL
{
    public class ClMetricGetByTimePeriodRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
