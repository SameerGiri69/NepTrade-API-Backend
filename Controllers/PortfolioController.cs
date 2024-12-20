using Finshark_API.Helpers;
using Finshark_API.Interfaces;
using Finshark_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockInterface _stockInterface;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fMPService;

        public PortfolioController(UserManager<AppUser> userManager, IStockInterface stockInterface,
            IPortfolioRepository portfolioRepository, IFMPService fMPService)
        {
            _userManager = userManager;
            _stockInterface = stockInterface;
            _portfolioRepository = portfolioRepository;
            _fMPService = fMPService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //var email = User.GetUserId();
            var currUser = await _userManager.GetUserAsync(User);


            var userPortfolio = _portfolioRepository.GetUserPortfolio(currUser);
            if (userPortfolio == null) return NotFound("Portfolio doesnot exists create one");
            return Ok(userPortfolio);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddPortfolio([FromQuery]string symbol)
        {
            var currUser = await _userManager.GetUserAsync(User);

            var stock = await _stockInterface.FindBySymbolAsync(symbol);

            if (stock == null)
            {
                var stockFromFMP = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stockFromFMP.Symbol == null)
                {
                    return BadRequest("stock doesnot exists");
                }
                var createdStock = _stockInterface.Create(stockFromFMP);
                stock = createdStock;
            }
            var userPortfolio =  _portfolioRepository.GetUserPortfolio(currUser);

            if (userPortfolio.Any(x => x.Symbol.ToUpper() == symbol.ToUpper())) 
                return BadRequest("The stock already exists in the user portfolio");

            var portfolioModel = new Portfolio()
            {
                AppUserId = currUser.Id,
                StockId = stock.Id
            };

            var portfolioResult = await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioResult == null) return StatusCode(500, "Could not add to portfolio");

            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio([FromQuery]string symbol)
        {
            var currUser = await _userManager.GetUserAsync(User);

            var userPortfolio =  _portfolioRepository.GetUserPortfolio(currUser);
            if (userPortfolio == null) return NotFound("Your portfolio is empty");

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToUpper() == symbol.ToUpper()).ToList();

            if(filteredStock.Count() >=1   )
            {
                await _portfolioRepository.DeletePortfolio(currUser, symbol);
            }
            else
                return BadRequest("Stock is not in your portfolio");

            return Ok();
        }
    }
}
