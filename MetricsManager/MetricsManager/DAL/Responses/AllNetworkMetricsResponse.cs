﻿using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Responses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<DotNetMetric> Metrics { get; set; }
    }
}
