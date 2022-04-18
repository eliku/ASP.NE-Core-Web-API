using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Request;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/agent/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _logger.LogDebug(4, "NLog встроен в MetricsAgent.NetworkMetricsController");
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpPost("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] NetworkMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(3,
                $"This log from Agent and GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }

            return Ok(response);
        }
    }
}
