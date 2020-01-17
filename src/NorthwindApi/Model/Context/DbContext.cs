using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Model.Context
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Conn { get; }
    }

    public class DbContext : IDbContext
    {
        private IDbConnection _conn;

        private readonly string _providerName;
        private readonly string _connectionString;

        public DbContext()
        {
            var server = @".\sqlserver2014";
            var dbName = "Northwind";
            var dbUser = "sa";
            var dbUserPass = "masuk";

            _providerName = "System.Data.SqlClient";
            _connectionString = $"Server={server};Database={dbName};User Id={dbUser};Password={dbUserPass};";
        }

        public IDbConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection(_providerName, _connectionString)); }
        }

        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            IDbConnection conn = null;

            try
            {
                var provider = DbProviderFactories.GetFactory(providerName);

                conn = provider.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch
            {
            }

            return conn;
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                }
                finally
                {
                    _conn.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
