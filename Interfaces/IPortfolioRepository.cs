using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface IPortfolioRepository
    {
        public List<Stock> GetUserPortfolio(AppUser user);
        public Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(AppUser user, string symbol);
    }
}
