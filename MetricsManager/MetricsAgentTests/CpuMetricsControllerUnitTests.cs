using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using AutoMapper;
using MetricsAgent;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private readonly CpuMetricsController controller;
        //private readonly ILogger<CpuMetricsController> logger;
        private readonly Mock<ICpuMetricsRepository> mock;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            controller = new CpuMetricsController(new Mock<ILogger <CpuMetricsController>>().Object , mock.Object, mapper);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� �����������, ��� � ����������� �������� CpuMetric - ������
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            // ��������� �������� �� �����������
            var result = controller.Create(new
            MetricsAgent.DAL.Request.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromHours(10),
                Value = 5
            });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // �������� ����� Create ����������� � ������ ����� ������� � ���������
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
    }
}
