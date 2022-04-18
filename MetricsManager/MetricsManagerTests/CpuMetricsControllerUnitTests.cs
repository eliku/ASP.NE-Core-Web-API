using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;


namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private readonly ILogger<CpuMetricsController> logger;

        public CpuMetricsControllerUnitTests()
        {
            controller = new CpuMetricsController(logger);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime,
            toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

}
