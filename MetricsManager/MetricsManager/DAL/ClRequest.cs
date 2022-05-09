using System;

namespace MetricsManager.DAL
{
    public class ClRequest
    {
        public string AgentUrl { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

    }

}
