namespace DhakaStockExchangeWorker.Services
{
    public interface IDSEService
    {
        void InsertData();
        bool IsMarketOpen();
    }
}
