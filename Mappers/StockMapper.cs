using Finshark_API.DTOs.Stock;
using Finshark_API.Models;

namespace Finshark_API.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            var stockDto = new StockDto()
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol,
            };
            return stockDto;
        }
    }
}
