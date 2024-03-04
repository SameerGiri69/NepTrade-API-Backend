using Finshark_API.DTOs.Stock;
using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface IStockInterface
    {
        public Stock GetStockById(int id);
        public IEnumerable<StockDto> GetAllStocks();
        public Stock Create(CreateStockDto stock);
        public Stock UpdateStock(UpdateStockDto updateDto, int id);
        public bool DeleteStock(int id);
        public bool Save();
    }
}
