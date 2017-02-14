using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Data.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(Domain.Repositories.ITodoRepository), typeof(Repositories.TodoRepository));
            dic.Add(typeof(Domain.Repositories.ITodoLogRepository), typeof(Repositories.TodoLogRepository));
            dic.Add(typeof(Domain.Repositories.IUnitOfWork), typeof(UnitOfWork));
            dic.Add(typeof(Domain.Repositories.IUnitOfWorkTransaction), typeof(UnitOfWorkTransaction));
            return dic;
        }
    }
}
