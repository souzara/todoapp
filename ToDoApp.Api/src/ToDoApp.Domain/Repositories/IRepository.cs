using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Repositories
{
    public interface IRepository { }

    public interface IRepository<T, TFiltro> : IRepository
    {
        T Create(T todo);
        IEnumerable<T> List(TFiltro filter);
        T GetById(int id);
        bool Update(T todo);
        bool Delete(int id);
    } 
}
