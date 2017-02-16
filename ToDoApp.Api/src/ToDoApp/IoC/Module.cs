using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.IoC
{
    public static class Module
    {

        public static List<Type> GetSingleTypes()
        {
            var result = new List<Type>();
            result.Add(typeof(Validators.TodoValidator));
            return result;
        }
    }
}
