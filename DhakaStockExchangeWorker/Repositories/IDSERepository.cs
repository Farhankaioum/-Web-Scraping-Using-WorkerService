using System.Collections.Generic;

namespace DhakaStockExchangeWorker.Repositories
{
    public interface IDSERepository
    {
        void InsertDatas(List<DSEModel> insertableData);
        void Save();
    }
}
