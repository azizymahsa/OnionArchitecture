using Domain.DataAccess.DataContext;
using Domain.Dto._Base;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataAccess.DataContext
{
    public class ConnectionKeeper : IConnectionKeeper
    {
        #region Constructor

        private readonly ApplicationSettingsDto _settings;
        private IDbConnection _objDbConnection;
        private bool _disposed;

        public ConnectionKeeper(ApplicationSettingsDto settings) => _settings = settings;

        #endregion

        #region New Connection

        public IDbConnection GetNewConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _settings.ConnectionStrings.Vtas ?? throw new ArgumentNullException(nameof(connectionString));
            }
            _objDbConnection = new OracleConnection(connectionString);
            return _objDbConnection;
        }

        #endregion

        #region IDisposable Support

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _objDbConnection.Dispose();
                // Free any other managed objects here.
            }
            _disposed = true;
        }

        #endregion
    }
}
