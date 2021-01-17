namespace backend.tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using backend.DatasetValidation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class TestFixture
    {
        public TestFixture()
        {
            var services = new ServiceCollection();

            var datasetValidators = GetAllImplementedInterfaces<IDatasetValidator>();
            datasetValidators.ForEach((t) => services.AddScoped(typeof(IDatasetValidator), t));
            services.AddTransient<IDatasetValidateProcessorFactory, DatasetValidateProcessorFactory>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        private List<Type> GetAllImplementedInterfaces<T>()
        {
            var assembly = typeof(IDatasetValidator).Assembly;
            return assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(T)))
                .Select(t => t.AsType())
                .ToList();
        }
    }
}
