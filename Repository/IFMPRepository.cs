using Finshark_API.DTOs.Stock;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Finshark_API.Models;
using Newtonsoft.Json;

namespace Finshark_API.Repository
{
    public class IFMPRepository : IFMPService
    {
        public IFMPRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private HttpClient _httpClient { get; set; }
        private IConfiguration _configuration { get; set; }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPkey"]}");
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if(stock != null)
                    {
                        return stock.ToStockFromFMP();
                    }
                    return null;
                }
                return null;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return null;

            }
            
        }
    }
}
