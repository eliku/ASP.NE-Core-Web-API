using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

//ОПЕРАТИВНАЯ ПАМЯТЬ (RAM), загруженность оперативной памяти;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(5, "NLog встроен в RamMetricsController");
        }

        //метод, который будут отдавать метрики в заданном диапазоне времени с определённого агента
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Запуск RamMetricsController.GetMetrics с параметрами: {fromTime}, {toTime} от {agentId}.");
            return Ok();
        }
        //метод, который будут отдавать метрики  в указанном периоде со всех агентов
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Получение RAM за период: {fromTime}, {toTime}");
            return Ok();
        }
    }
}
