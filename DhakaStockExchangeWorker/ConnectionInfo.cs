using Microsoft.Extensions.Configuration;

namespace DhakaStockExchangeWorker
{
    public static class ConnectionInfo
    {
        private static IConfiguration _configuration;
        private static string _connectionString = string.Empty;

        public static string ConnectionString { get; set; } = GetConnectionString();

        private static string GetConnectionString()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            return _connectionString;
        }
    }
}
