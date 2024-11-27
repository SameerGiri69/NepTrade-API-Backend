using Finshark_API.DTOs.Stock;
using Finshark_API.Helpers;
using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface IStockInterface
    {
        Stock GetStockById(int id);
        Task<Stock> FindBySymbolAsync(string symbol);
        IEnumerable<Stock> GetAllStocks(QueryObject query);
        Stock Create(Stock stock);
        Stock UpdateStock(UpdateStockDto updateDto, int id);
        bool DeleteStock(int id);
        bool Save();
        Task<bool> StockExists(int id);
    }
}
