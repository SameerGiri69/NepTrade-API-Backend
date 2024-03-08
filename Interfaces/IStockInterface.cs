using Finshark_API.DTOs.Stock;
using Finshark_API.Helpers;
using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface IStockInterface
    {
        Stock GetStockById(int id);
        IEnumerable<Stock> GetAllStocks(QueryObject query);
        Stock Create(CreateStockDto stock);
        Stock UpdateStock(UpdateStockDto updateDto, int id);
        bool DeleteStock(int id);
        bool Save();
        Task<bool> StockExists(int id);
    }
}
