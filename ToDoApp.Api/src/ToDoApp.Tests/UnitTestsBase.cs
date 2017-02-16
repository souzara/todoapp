using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Tests
{
    public abstract class UnitTestsBase
    {
        private IServiceProvider provider;
        private ServiceCollection serviceCollection;
        private static bool autoMapperIsInitialized = false;
        public UnitTestsBase()
        {
            InitializeAutomapper();
            ConfigureIoC();
        }

        private void InitializeAutomapper()
        {
            if (autoMapperIsInitialized)
                return;
            Mappings.AutoMapperConfiguration.Initialize();
            autoMapperIsInitialized = true;
        }

        private void ConfigureIoC()
        {
            serviceCollection = new ServiceCollection();
            IoC.IoCConfiguration.Configure(serviceCollection);
            provider = serviceCollection.BuildServiceProvider();
        }

        public T InstanceOf<T>()
        {
            return provider.GetService<T>();
        }

        public void OverrideRegistration<T>(Func<IServiceProvider, T> implementationFactory)
            where T : class
        {
            var serviceDecriptor = serviceCollection.FirstOrDefault(x => x.ImplementationType == typeof(T));
            if (serviceCollection != null)
                serviceCollection.Remove(serviceDecriptor);

            serviceCollection.AddScoped<T>(implementationFactory);
            provider = serviceCollection.BuildServiceProvider();
        }

    }
}
