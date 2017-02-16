using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Repositories;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Data
{
    public class UnitOfWork : Domain.Repositories.IUnitOfWork
    {
        #region Fields 
        private readonly IDbConnection connection;

        #endregion
        
        #region Constructor
        public UnitOfWork(IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetSection(RepositoryBase.CONNECTIONSTRING_KEY);

            if (string.IsNullOrWhiteSpace(connectionString.Value))
                throw new ArgumentNullException("Connection string not found");


            connection = new SqlConnection(connectionString.Value);
        } 
        #endregion
 
        #region Begin
        public IUnitOfWorkTransaction Begin(params IRepository[] repositories)
        {

            if (repositories == null || repositories.Length == 0)
                throw new ArgumentNullException(nameof(repositories), "repositories is required");
            var unitOfWorkTransaction = new UnitOfWorkTransaction(connection, repositories);
            return unitOfWorkTransaction;
        }
        #endregion
    }
}
