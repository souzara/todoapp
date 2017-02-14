using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Repositories
{
    public interface ITodoLogRepository : IRepository
    {
        Entities.TodoLog Create(Entities.TodoLog todo);
    }
}
