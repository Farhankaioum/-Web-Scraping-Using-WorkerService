using DhakaStockExchangeWorker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DhakaStockExchangeWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDSEService _dSEService;

        public Worker(ILogger<Worker> logger, IDSEService dSEService)
        {
            _logger = logger;
            _dSEService = dSEService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                if (_dSEService.IsMarketOpen())
                {
                    try
                    {
                        _dSEService.InsertData();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, ex);
                    }
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                await Task.Delay(elapsedMs > 0 ? (int)(55000 - elapsedMs) : 0, stoppingToken);
                watch.Reset();
            }
        }

    }
}
