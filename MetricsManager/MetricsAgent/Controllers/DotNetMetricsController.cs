using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Request;
using MetricsAgent.DAL.Responses;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/agent/dotnet/errors-count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private  ILogger<DotNetMetricsController> _logger;
        private  IDotNetMetricsRepository _repository;
        private  IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _logger.LogDebug(2, "NLog встроен в MetricsAgent.DotNetMetricsController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }


        [HttpPost("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] DotNetMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(2,
                $"This log from Agent and GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
