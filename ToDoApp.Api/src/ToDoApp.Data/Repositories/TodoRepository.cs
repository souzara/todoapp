using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Filters;
using Dapper;

namespace ToDoApp.Data.Repositories
{
    internal class TodoRepository : RepositoryBase, Domain.Repositories.ITodoRepository
    {
        public TodoRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public Todo Create(Todo todo)
        {
            todo.Id = Connection.QueryFirst<int>("exec todo_sp_create @Text, @IsCompleted", todo, transaction: transaction);
            return todo;
        }

        public bool Delete(int id)
        {
            var affectedRows = Connection.Execute("Exec todo_sp_delete @Id", new { Id = id }, transaction: transaction);

            return affectedRows > 0;
        }

        public Todo GetById(int id)
        {
            var result = Connection.QueryFirstOrDefault<Todo>("exec todo_sp_get @Id", new { Id = id }, transaction: transaction);
            return result;
        }

        public IEnumerable<Todo> List(TodoFilter filter)
        {
            var result = Connection.Query<Todo>("exec todo_sp_list @Id, @Text, @IsCompleted", filter, transaction: transaction);

            return result;
        }

        public bool Update(Todo todo)
        {
            var affectedRows = Connection.Execute("exec todo_sp_update @Id, @Text, @IsCompleted", todo, transaction: transaction);
            return affectedRows > 0;
        }
    }
}
