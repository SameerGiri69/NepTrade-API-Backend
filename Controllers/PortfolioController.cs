using Finshark_API.Helpers;
using Finshark_API.Interfaces;
using Finshark_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark_API.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    [Authorize]
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockInterface _stockInterface;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockInterface stockInterface,
            IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockInterface = stockInterface;
            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //var email = User.GetUserId();
            var email = HttpContext.User.GetUserId();
            var user = await _userManager.FindByNameAsync(email);


            var userPortfolio = _portfolioRepository.GetUserPortfolio(user);
            if (userPortfolio == null) return NotFound("Portfolio doesnot exists create one");
            return Ok(userPortfolio);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var email = HttpContext.User.GetUserId();
            var user = await _userManager.FindByEmailAsync(email);

            var stock = await _stockInterface.FindBySymbolAsync(symbol);

            if (stock == null) return NotFound("The stock doesn't exists");

            var userPortfolio =  _portfolioRepository.GetUserPortfolio(user);

            if (userPortfolio.Any(x => x.Symbol.ToUpper() == symbol.ToUpper())) 
                return BadRequest("The stock already exists in the user portfolio");

            var portfolioModel = new Portfolio()
            {
                AppUserId = user.Id,
                StockId = stock.Id
            };

            var portfolioResult = await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioResult == null) return StatusCode(500, "Could not add to portfolio");

            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var email = HttpContext.User.GetUserId();
            var user = await _userManager.FindByEmailAsync(email);

            var userPortfolio =  _portfolioRepository.GetUserPortfolio(user);
            if (userPortfolio == null) return NotFound("Your portfolio is empty");

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToUpper() == symbol.ToUpper()).ToList();

            if(filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(user, symbol);
            }
            else
                return BadRequest("Stock is not in your portfolio");

            return Ok();
        }
    }
}
