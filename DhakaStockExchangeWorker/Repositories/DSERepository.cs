using System;
using System.Collections.Generic;

namespace DhakaStockExchangeWorker.Repositories
{
    public class DSERepository : IDSERepository, IDisposable
    {
        private readonly WorkerContext _context;

        public DSERepository(WorkerContext context)
        {
            _context = context;
        }

        public void InsertDatas(List<DSEModel> insertableData)
        {
            _context.DSEModels.AddRange(insertableData);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
