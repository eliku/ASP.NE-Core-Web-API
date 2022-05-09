using System.Data.SQLite;

namespace MetricsManager.DAL
{
    interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}
