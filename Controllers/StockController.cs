using Finshark_API.Data;
using Finshark_API.DTOs.Stock;
using Finshark_API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finshark_API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.stocks.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _context.stocks.FindAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var stockmodel = stockDto.ToStockFromCreateDto();
            _context.Add(stockmodel);
            _context.SaveChanges();
            // Create at action invokes the GetStockById and passes in the id then after it gets the stock object it returns StockDto after mapping
            return CreatedAtAction(nameof(GetStockById), new { id = stockmodel.Id }, stockmodel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            var myStock = _context.stocks.Find(id);
            if (myStock == null) return NotFound();

            myStock.Symbol = updateDto.Symbol;
            myStock.MarketCap = updateDto.MarketCap;
            myStock.Purchase = updateDto.Purchase;
            myStock.LastDiv = updateDto.LastDiv;
            myStock.CompanyName = updateDto.CompanyName;
            myStock.Industry = updateDto.Industry;

            _context.SaveChanges();

            return Ok(myStock.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _context.stocks.Find(id);

            if (stock == null) return NotFound();

            _context.Remove(stock);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
