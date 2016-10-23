using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.AppServices.Dtos;

namespace ToDoApp.AppServices.Interfaces
{
    public interface ITodoAppService
    {
        TodoDto Create(TodoDto todo);
        IEnumerable<TodoDto> List(TodoFilterDto filter);
        TodoDto GetById(int id);
        bool Update(TodoDto todo);
        bool Delete(int id);
    }
}
