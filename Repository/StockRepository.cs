using Finshark_API.Data;
using Finshark_API.DTOs.Stock;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Finshark_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Finshark_API.Repository
{
    public class StockRepository : IStockInterface
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Stock Create(CreateStockDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            _context.Add(stock);
            Save();
            return _context.stocks.Find(stock.Id);
        }

        public bool DeleteStock(int id)
        {
            var stock = _context.stocks.Find(id);
            if (stock == null) return false;

            _context.Remove(stock);

            return Save();
        }

        public IEnumerable<StockDto> GetAllStocks()
        {
            var stocks =  _context.stocks.AsEnumerable().Select(s=>s.ToStockDto());
            return stocks;
        }

        public Stock GetStockById(int id)
        {
            var stock = _context.stocks.Find(id);
            return stock;
        }

        public Stock UpdateStock( UpdateStockDto updateDto, int id)
        {
            var myStock = _context.stocks.Find(id); 
            myStock.Symbol = updateDto.Symbol;
            myStock.MarketCap = updateDto.MarketCap;
            myStock.Purchase = updateDto.Purchase;
            myStock.LastDiv = updateDto.LastDiv;
            myStock.CompanyName = updateDto.CompanyName;
            myStock.Industry = updateDto.Industry;

             Save();
            return myStock;
        }
        public bool Save()
        {
            var result =  _context.SaveChanges();
            if (result < 0) return false;
            return true;
        }
    }
}
