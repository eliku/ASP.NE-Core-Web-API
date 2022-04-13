using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

//ЦП (CPU), загруженность процессора;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        //метод, который будут отдавать метрики в заданном диапазоне времени с определённого агента
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Запуск CpuMetricsController.GetMetrics с параметрами: {fromTime}, {toTime} от {agentId}.");
            return Ok();
        }
        //метод, который будут отдавать метрики  в указанном периоде со всех агентов
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Получение показателей ЦП за период: {fromTime}, {toTime}");
            return Ok();
        }
    }
}
