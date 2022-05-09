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
    public interface IAgentRepository 
    {
        void Create(AgentInfo agent);
        IList<AgentInfo> Get();
        AgentInfo GetById(int id);
        void Update(AgentInfo agent);
    }

    public class AgentRepository : IAgentRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public void Create(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                connection.Execute("INSERT INTO agents (uri,isenabled) VALUES (@uri,@isenabled);",
                new
                {
                    uri = agent.Url,
                    isenabled = agent.IsEnabled
                }
                );
            }
        }

        public IList<AgentInfo> Get()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                IList<AgentInfo> result = connection.Query<AgentInfo>("SELECT * FROM agents").ToList();

                return result;
            }
        }

        public AgentInfo GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<AgentInfo>("SELECT * FROM agents WHERE id=@id",
                    new { id });
            }
        }

        public void Update(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE agents SET Enabled = @state WHERE AgentId = @agentId",
                    new
                    {
                        agent = agent
                    });
            }
        }
    }
}