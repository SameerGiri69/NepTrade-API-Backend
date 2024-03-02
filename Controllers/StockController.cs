using Finshark_API.Data;
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
        public async Task<IActionResult> GetStockById([FromRoute]int id)
        {
            var stock = await _context.stocks.FindAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }
       
    }
}
