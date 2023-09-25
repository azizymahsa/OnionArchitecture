using System.Data;

namespace Domain.DataAccess.DataContext
{
    public interface IConnectionKeeper : IDisposable
    {
        IDbConnection GetNewConnection(string connectionString = null);
    }
}
