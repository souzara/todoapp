using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Repositories
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
