using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ISalesRecordCollection
    {
        Task InsertSalesRecord(SalesRecord SalesRecord);
        Task UpdateSalesRecord(SalesRecord SalesRecord);
        Task DeleteSalesRecord(string id);
        Task<List<SalesRecord>> GetAllSalesRecords();
        Task<SalesRecord> GetSalesRecordById(string id);
    }
}
