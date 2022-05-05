using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    // Маркировочный интерфейс
    // используется, чтобы проверять работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
    }

    public class CpuMetricsRepository: ICpuMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор
       
        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute("INSERT INTO cpumetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
            // Анонимный объект с параметрами запроса
            new
            {

                AgentId = item.AgentId,
                value = item.Value,
                // Записываем в поле time количество секунд
                time = item.Time.TotalSeconds
            });

        } 
        public IList<CpuMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {

            using var connection = new SQLiteConnection(ConnectionString);
            // Читаем, используя Query, и в шаблон подставляем тип данных,
            // объект которого Dapper, он сам заполнит его поля
            // в соответствии с названиями колонок
            return connection.Query<CpuMetric>("SELECT id, value, time FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime").ToList();

        }

        public TimeSpan GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.ExecuteScalar<long>("SELECT Max(Time) FROM cpumetrics WHERE agentId = @agentId",

                new { agentId });

                return TimeSpan.Parse(result.ToString());

            }
        }

        public IList<CpuMetric> GetByTimePeriod(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetric>(" SELECT * FROM cpumetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                new
                {
                    agentId = agentId,
                    fromTime = fromTime.TotalMilliseconds,
                    toTime = toTime.TotalMilliseconds
                }).ToList();
            }
        }
    }
}