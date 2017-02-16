using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            foreach (var type in IoC.Module.GetSingleTypes())
                services.AddScoped(type);
            Bootstraper.Configure(services);
        }
    }
}
