using Microsoft.EntityFrameworkCore;

namespace DhakaStockExchangeWorker
{
    public class WorkerContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public WorkerContext()
        {
            _connectionString = ConnectionInfo.ConnectionString;
            _assemblyName = typeof(Program).Assembly.FullName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_assemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DSEModel> DSEModels { get; set; }
    }
}
