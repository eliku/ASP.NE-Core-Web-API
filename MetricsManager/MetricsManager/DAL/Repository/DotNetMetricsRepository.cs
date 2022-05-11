﻿using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    // Маркировочный интерфейс
    // используется, чтобы проверять работу репозитория на тесте-заглушке
    public interface IDotNetMetricsRepository:IRepository<DotNetMetric>
    {
    }

    public class DotNetMetricsRepository:IDotNetMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий через конструктор


        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute("INSERT INTO dotnetmetrics(agentId, value, time) VALUES(@value, @time, @agentId)",
            // Анонимный объект с параметрами запроса
            new
            {
                agentId = item.AgentId,
                value = item.Value,
                // Записываем в поле time количество секунд
                time = item.Time.TotalSeconds
            });
        }

        public IList<DotNetMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Читаем, используя Query, и в шаблон подставляем тип данных,
            // объект которого Dapper, он сам заполнит его поля
            // в соответствии с названиями колонок
            return connection.Query<DotNetMetric>("SELECT id, value, time FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime").ToList();
        }
        public TimeSpan GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.ExecuteScalar<long>("SELECT Max(Time) FROM dotnetmetrics WHERE agentId = @agentId",

                new { agentId });

                return TimeSpan.Parse(result.ToString());

            }
        }

        public IList<DotNetMetric> GetByTimePeriod(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>(" SELECT * FROM dotnetmetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
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