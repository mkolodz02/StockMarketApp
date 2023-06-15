﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockMarketApp.Shared.Models.DTOs;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace StockMarketApp.Server.Services
{

    public interface IStocksService
    {
        Task<CompanyMainDataDto> GetStockInfo(string ticker);
        Task<List<CompanyTickerNameDto>> GetStockList(string tickerPart);
        Task<AllOhlcDataDto> GetOhlcData(string dateFrom, string dateTo, string ticker);
        Task<string> GetResponseBody(string url);
        Task<DailyPricesDto> GetDailyPrices(string ticker, string dateFrom);
    }

    public class StocksService : IStocksService
    {
        public async Task<AllOhlcDataDto> GetOhlcData(string dateFrom, string dateTo, string ticker)
        {
            string url = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{dateFrom}/{dateTo}?adjusted=true&sort=asc&limit=120&apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);
            var jsonObject = JObject.Parse(responseBody);

            var resultsList = JsonConvert.DeserializeObject<List<OhlcData>>(jsonObject["results"].ToString());

            return new AllOhlcDataDto
            {
                Ticker = Convert.ToString(jsonObject["ticker"]),
                QueryCount = Convert.ToInt32(jsonObject["queryCount"]),
                ResultsCount = Convert.ToInt32(jsonObject["resultsCount"]),
                Adjusted = (bool)jsonObject["adjusted"],
                Results = resultsList, //DO ZMIANY
                Status = Convert.ToString(jsonObject["status"]),
                RequestId = Convert.ToString(jsonObject["request_id"]),
                Count = Convert.ToInt32(jsonObject["count"])
            };
        }

        

        public async Task<CompanyMainDataDto> GetStockInfo(string ticker)
        {
            string url = $"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey=XjtXTzHcSpBZy3bYsKF1fw4GeFe1VpKg";

            string responseBody = await GetResponseBody(url);

            var jsonObject = JObject.Parse(responseBody);

            return new CompanyMainDataDto
            {
                Ticker = Convert.ToString(jsonObject["results"]["ticker"]),
                Logo = Convert.ToString(jsonObject["results"]["branding"]["logo_url"]),
                Name = Convert.ToString(jsonObject["results"]["name"]),
                City = Convert.ToString(jsonObject["results"]["address"]["city"])

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

            var jsonObject = JObject.Parse(responseBody);

            return new DailyPricesDto
            {
                Status = Convert.ToString(jsonObject["status"]),
                From = Convert.ToString(jsonObject["from"]),
                Symbol = Convert.ToString(jsonObject["symbol"]),
                Open = Convert.ToDouble(jsonObject["open"]),
                High = Convert.ToDouble(jsonObject["high"]),
                Low = Convert.ToDouble(jsonObject["low"]),
                Close = Convert.ToDouble(jsonObject["close"]),
                Volume = Convert.ToDouble(jsonObject["volume"]),
                AfterHours = Convert.ToDouble(jsonObject["afterHours"]),
                PreMarket = Convert.ToDouble(jsonObject["preMarket"]),
            };
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
                        throw new Exception("Niepoprawny status");
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