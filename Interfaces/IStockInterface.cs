using Finshark_API.DTOs.Stock;
using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface IStockInterface
    {
        public Stock GetStockById(int id);
        public IEnumerable<StockDto> GetAllStocks();
        public Stock Create(CreateStockDto stock);
        public Stock UpdateStock(Stock stock, UpdateStockDto updateDto);
        public bool DeleteStock(int id);
        public bool Save();
    }
}
