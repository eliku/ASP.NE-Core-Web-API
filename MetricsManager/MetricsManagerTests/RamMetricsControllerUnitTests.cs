using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private readonly ILogger<RamMetricsController> logger;

        public RamMetricsControllerUnitTests()
        {
            controller = new RamMetricsController(logger);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(50);
            var toTime = TimeSpan.FromSeconds(200);
            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime,
            toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}
