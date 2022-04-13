using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private readonly ILogger<NetworkMetricsController> logger;

        public NetworkMetricsControllerUnitTests()
        {
            controller = new NetworkMetricsController(logger);
        }
        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(20);
            var toTime = TimeSpan.FromSeconds(200);
            //Act
            var result = controller.GetMetrics(fromTime,
            toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
