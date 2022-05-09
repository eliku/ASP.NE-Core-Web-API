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
    public interface INetworkMetricsRepository:IRepository<NetworkMetric>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute("INSERT INTO networkmetrics(agentId, value, time) VALUES(@value, @time, @agentId)",
            // Анонимный объект с параметрами запроса
            new
            {
                agentId = item.AgentId,
                value = item.Value,
                // Записываем в поле time количество секунд
                time = item.Time.TotalSeconds
            });
        }

        public IList<NetworkMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Читаем, используя Query, и в шаблон подставляем тип данных,
            // объект которого Dapper, он сам заполнит его поля
            // в соответствии с названиями колонок
            return connection.Query<NetworkMetric>("SELECT id, value, time FROM networknetmetrics WHERE time BETWEEN @fromTime AND @toTime").ToList();
        }
        public TimeSpan GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.ExecuteScalar<long>("SELECT Max(Time) FROM networknetmetrics WHERE agentId = @agentId",

                new { agentId });

                return TimeSpan.Parse(result.ToString());

            }
        }

        public IList<NetworkMetric> GetByTimePeriod(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetric>(" SELECT * FROM networknetmetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
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