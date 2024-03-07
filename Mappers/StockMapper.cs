using Finshark_API.DTOs.Stock;
using Finshark_API.Models;

namespace Finshark_API.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto()
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol,
                Comments = stock.Comments.Select(c=>c.ToCommentDtoFromComment()).ToList()
                
            };
            
        }
        public static Stock ToStockFromCreateDto(this CreateStockDto dtoStock)
        {
            return new Stock()
            {
                CompanyName = dtoStock.CompanyName,
                Symbol = dtoStock.Symbol,
                Industry = dtoStock.Industry,
                LastDiv = dtoStock.LastDiv,
                MarketCap = dtoStock.MarketCap,
                Purchase = dtoStock.Purchase,
            };
            
        }
        public static Stock ToStockFromUpdateDto(this UpdateStockDto dtoStock)
        {
            return new Stock()
            {
                CompanyName = dtoStock.CompanyName,
                Symbol = dtoStock.Symbol,
                Industry = dtoStock.Industry,
                LastDiv = dtoStock.LastDiv,
                MarketCap = dtoStock.MarketCap,
                Purchase = dtoStock.Purchase,
            };
        }
    }
}
