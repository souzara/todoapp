using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Filters;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.DomainServices
{
    internal class TodoDomainService : Interfaces.ITodoDomainService
    {
        private readonly ITodoRepository repository;
        private readonly ITodoLogRepository todoLogRepository;
        private readonly IUnitOfWork uow;

        public TodoDomainService(ITodoRepository repository, ITodoLogRepository todoLogRepository, IUnitOfWork uow)
        {
            this.repository = repository;
            this.todoLogRepository = todoLogRepository;
            this.uow = uow;
        }
        public Todo Create(Todo todo)
        {
            Todo result;
            using (var uowTransaction = this.uow.Begin(repository, todoLogRepository))
            {
                try
                {
                    result = repository.Create(todo);
                    foreach (var todoLog in result.TodoLogs ?? Enumerable.Empty<TodoLog>())
                    {
                        todoLog.TodoId = result.Id;
                        todoLogRepository.Create(todoLog);
                    }
                    uowTransaction.Commit();
                }
                catch
                {
                    uowTransaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public Todo GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Todo> List(TodoFilter filter)
        {
            return repository.List(filter);
        }

        public bool Update(Todo todo)
        {
            return repository.Update(todo);
        }
    }
}
