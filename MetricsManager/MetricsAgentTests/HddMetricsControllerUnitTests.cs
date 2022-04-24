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
    public class HddMetricsControllerUnitTests
    {
        private readonly HddMetricsController controller;
        private readonly Mock<IHddMetricsRepository> mock;

        public HddMetricsControllerUnitTests()
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp
            .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            controller = new HddMetricsController(new Mock<ILogger<HddMetricsController>>().Object, mock.Object, mapper);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = controller.Create(new
            MetricsAgent.DAL.Request.HddMetricCreateRequest
            {
                Time = TimeSpan.FromHours(10),
                Value = 5
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
    }
}
