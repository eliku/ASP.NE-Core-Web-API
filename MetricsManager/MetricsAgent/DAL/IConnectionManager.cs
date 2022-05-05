using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}
