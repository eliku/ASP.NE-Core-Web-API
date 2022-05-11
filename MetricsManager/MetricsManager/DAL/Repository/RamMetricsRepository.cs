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
    public interface IRamMetricsRepository:IRepository<RamMetric>
    {
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute("INSERT INTO rammetrics(agentId, value, time) VALUES(@value, @time, @agentId)",
            // Анонимный объект с параметрами запроса
            new
            {
                agentId = item.AgentId,
                value = item.Value,
                // Записываем в поле time количество секунд
                time = item.Time.TotalSeconds
            });
        }

        public IList<RamMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Читаем, используя Query, и в шаблон подставляем тип данных,
            // объект которого Dapper, он сам заполнит его поля
            // в соответствии с названиями колонок
            return connection.Query<RamMetric>("SELECT id, value, time FROM ramknetmetrics WHERE time BETWEEN @fromTime AND @toTime").ToList();
        }

        public TimeSpan GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.ExecuteScalar<long>("SELECT Max(Time) FROM ramknetmetrics WHERE agentId = @agentId",

                new { agentId });

                return TimeSpan.Parse(result.ToString());

            }
        }

        public IList<RamMetric> GetByTimePeriod(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>(" SELECT * FROM ramknetmetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
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