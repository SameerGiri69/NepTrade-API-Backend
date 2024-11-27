using Finshark_API.Data;
using Finshark_API.DTOs.Stock;
using Finshark_API.Helpers;
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
        public Stock Create(Stock stock)
        {
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

        public IEnumerable<Stock> GetAllStocks(QueryObject query)
        {
            // If there is no query input form user then the method returns all the stocks 
            // if there is symbol or company input the method returns the stocks which contains the 
            // specified company / symbol 
            // If sortBy is equals "Symbol" then the method sorts the element from the company/symbol query
            // in ascending or descending order as the user specifies 
            var stocks = _context.stocks.Include(c => c.Comments).ThenInclude(a=>a.AppUser).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
    
            // Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;   

            return stocks.Skip(skipNumber).Take(query.PageSize);
        }

        public Stock GetStockById(int id)
        {
            var stock = _context.stocks.Include(c => c.Comments).ThenInclude(a=>a.AppUser).FirstOrDefault(x=>x.Id == id);
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

        public async Task<bool> StockExists(int id)
        {
            return await _context.stocks.AnyAsync(s=>s.Id == id);
        }

        public async Task<Stock> FindBySymbolAsync(string symbol)
        {
            var stock = await _context.stocks.Where(s => s.Symbol == symbol.ToUpper()).FirstOrDefaultAsync();

            return stock;
        }
    }
}
