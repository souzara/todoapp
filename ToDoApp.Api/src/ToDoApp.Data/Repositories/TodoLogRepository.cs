using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDoApp.Domain.Entities;
using Dapper;

namespace ToDoApp.Data.Repositories
{
    internal class TodoLogRepository : RepositoryBase, Domain.Repositories.ITodoLogRepository
    {
        public TodoLogRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public TodoLog Create(TodoLog todo)
        {
            todo.Id = Connection.QueryFirst<int>("exec todoLog_sp_create @Description, @TodoId", todo, transaction: transaction);
            return todo;
        }
    }
}
