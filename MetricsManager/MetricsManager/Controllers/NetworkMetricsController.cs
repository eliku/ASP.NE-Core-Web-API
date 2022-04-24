using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

//Сеть (Network), загруженность сети.

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController:ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(4, "NLog встроен в NetworkMetricsController");
        }

        //метод, который будут отдавать метрики в заданном диапазоне времени с определённого агента
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Запуск NetworkMetricsController.GetMetrics с параметрами: {fromTime}, {toTime} от {agentId}.");
            return Ok();
        }

        //метод, который будут отдавать метрики  в указанном периоде со всех агентов
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Получение загруженности сети за период: {fromTime}, {toTime}");
            return Ok();
        }
    }
}
