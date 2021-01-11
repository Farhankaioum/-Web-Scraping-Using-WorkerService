using Autofac;
using DhakaStockExchangeWorker.Repositories;
using DhakaStockExchangeWorker.Services;
using Microsoft.Extensions.Configuration;

namespace DhakaStockExchangeWorker
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public WorkerModule(string connectionStringName, string migrationAssemblyName,
            IConfiguration configuration)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorkerContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DSERepository>().As<IDSERepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DSEService>().As<IDSEService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
