using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ToDoApp.Data.Repositories
{
    internal class RepositoryBase
    {
        internal const string CONNECTIONSTRING_KEY = "ConnectionString";

        private IDbConnection connection;
        internal IDbConnection Connection
        {
            get
            {
                return connection;
            }

            set
            {
                connection = value;
            }
        }

        public RepositoryBase(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection(CONNECTIONSTRING_KEY);
            if (string.IsNullOrWhiteSpace(connectionString.Value))
                throw new ArgumentNullException("Connection string not found");
            Connection = new SqlConnection(connectionString.Value);
        }

        public IDbTransaction transaction { get; private set; }

        

        public void SetTransaction(IDbTransaction transaction)
        {
            this.transaction = transaction;
            this.connection = transaction.Connection;
        }

        internal void SetConnection(IDbConnection connection)
        {
            this.connection = connection;
            this.transaction = null;
        }
    }
}
