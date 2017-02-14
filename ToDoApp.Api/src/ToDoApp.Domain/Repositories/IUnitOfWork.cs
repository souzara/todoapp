using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUnitOfWorkTransaction Begin(params IRepository[] repositories);
    }
}
