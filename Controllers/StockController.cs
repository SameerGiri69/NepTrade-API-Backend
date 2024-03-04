using Finshark_API.Data;
using Finshark_API.DTOs.Stock;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finshark_API.Controllers
{
    [Route("api/stock")]
    [ApiController]    
    public class StockController : Controller
    {
        private readonly IStockInterface _stockInterface;

        public StockController(IStockInterface stockInterface)
        {
            _stockInterface = stockInterface;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _stockInterface.GetAllStocks();
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _stockInterface.GetStockById(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto)
        {
            var result = _stockInterface.Create(stockDto);
                
            // Create at action invokes the GetStockById and passes in the id then after it gets the stock object it returns StockDto after mapping
            return CreatedAtAction(nameof(GetStockById), new { id = result.Id }, result.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            try
            { 
                var result = _stockInterface.UpdateStock(updateDto, id);

                return Ok(result.ToStockDto());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {

            var result = _stockInterface.DeleteStock(id);
            if (result == false) return BadRequest(); 

            return NoContent();

        }
    }
}
