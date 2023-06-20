using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockMarketApp.Shared.Models.DTOs;
using StockMarketApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using StockMarketApp.Shared.Models;

namespace StockMarketApp.Server.Services
{
    public interface IStocksService
    {
        Task<CompanyMainDataDto> GetStockInfo(string ticker);
        Task<List<CompanyTickerNameDto>> GetStockList(string tickerPart);
        Task<List<OhlcData>> GetOhlcData(string ticker, string dateFrom, string dateTo);
        Task<string> GetResponseBody(string url);
        Task<DailyPricesDto> GetDailyPrices(string ticker, string dateFrom);
        Task<bool> IsCompInDatabase(string ticker);
        Task AddCompToDatabase(CompanyMainDataDto company);
        Task<CompanyMainDataDto> GetStockInfoFromDb(string ticker);
    }

    public class StocksService : IStocksService
    {

        private readonly ApplicationDbContext _context;

        public StocksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OhlcData>> GetOhlcData(string ticker, string dateFrom, string dateTo)
        {
            string url = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{dateFrom}/{dateTo}?adjusted=true&sort=asc&limit=120&apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);
            var jsonObject = JObject.Parse(responseBody);

            var resultsList = JsonConvert.DeserializeObject<List<OhlcData>>(jsonObject["results"].ToString());

            for (int i = 0; i < resultsList.Count; i++)
            {
                resultsList[i].Date = DateTime.Parse(dateFrom).AddDays(i);
                resultsList[i].Date = resultsList[i].Date.Date;
            }

            return resultsList;
        }


        public async Task<CompanyMainDataDto> GetStockInfo(string ticker)
        {
            string url = $"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);

            return JsonConvert.DeserializeObject<CompanyMainDataDto>(responseBody);
        }

        public async Task AddCompToDatabase(CompanyMainDataDto company)
        {

            var compTicker = company.Results.Ticker is null ? "no data" : company.Results.Ticker;
            var compLogo = company.Results.Branding is null ? "no data" : company.Results.Branding.Logo_url;
            var compName = company.Results.Name is null ? "no data" : company.Results.Name;
            var compCity = company.Results.Address is null ? "no data" : company.Results.Address.City;

            var cachedMainData = new CachedMainData()
            {
                Ticker = compTicker,
                Logo = compLogo,
                Name = compName,
                City = compCity,
            };

            await _context.AddAsync(cachedMainData);
            await _context.SaveChangesAsync();

        }

        public async Task<CompanyMainDataDto> GetStockInfoFromDb(string ticker)
        {
            var info = await _context.MainCompanyData.FirstAsync(i => i.Ticker == ticker);
            return new CompanyMainDataDto
            {
                Results = new MainResultsDto()
                {
                    Ticker = info.Ticker,
                    Name = info.Name,
                    Address = new AddressDto()
                    {
                        City = info.City
                    },
                    
                    Branding = new BrandingDto()
                    {
                        Logo_url = info.Logo
                    }
                },
                
            };
        }

        public async Task<List<CompanyTickerNameDto>> GetStockList(string tickerPart)
        {
            var resultList = new List<CompanyTickerNameDto>();
            string url = $"https://api.polygon.io/v3/reference/tickers?search={tickerPart}&apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);

            var jsonObject = JObject.Parse(responseBody);

            resultList = JsonConvert.DeserializeObject<List<CompanyTickerNameDto>>(jsonObject["results"].ToString());
            return resultList;
        }

        public async Task<DailyPricesDto> GetDailyPrices(string ticker, string dateFrom)
        {
            string url = $"https://api.polygon.io/v1/open-close/{ticker}/{dateFrom}?adjusted=true&apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);

            return JsonConvert.DeserializeObject<DailyPricesDto>(responseBody);
        }

        public async Task<bool> IsCompInDatabase(string ticker)
        {
            var comp = await _context.MainCompanyData.FirstOrDefaultAsync(c => c.Ticker == ticker);
            return comp != null;
        }

        public async Task<string> GetResponseBody(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.IsSuccessStatusCode)
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception($"Request failed. Error status code: {(int) resp.StatusCode}");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        
    }
}
