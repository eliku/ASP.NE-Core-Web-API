using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Moq;
using AutoMapper;
using MetricsAgent;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkMetricsController controller;
        private readonly Mock<INetworkMetricsRepository> mock;

        public NetworkMetricsControllerUnitTests()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
           .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            controller = new NetworkMetricsController(new Mock<ILogger<NetworkMetricsController>>().Object, mock.Object, mapper);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.DAL.Request.NetworkMetricCreateRequest
            {
                Time = TimeSpan.FromHours(10),
                Value = 5
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}
