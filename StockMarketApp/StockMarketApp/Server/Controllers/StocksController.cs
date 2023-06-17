using Microsoft.AspNetCore.Mvc;
using StockMarketApp.Server.Services;

namespace StockMarketApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {

        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        [HttpGet("StockInfo/{ticker}")]
        public async Task<IActionResult> GetStockInfo(string ticker)
        {
            return Ok(await _stocksService.GetStockInfo(ticker));
        }

        [HttpGet("StocksList/{ticker}")]
        public async Task<IActionResult> GetStockList(string ticker)
        {
            return Ok(await _stocksService.GetStockList(ticker));
        }

        [HttpGet("OhlcData/{ticker}/{dateFrom}/{dateTo}")] // ???
        public async Task<IActionResult> GetOhlcData(string ticker, string dateFrom, string dateTo)
        {
            return Ok(await _stocksService.GetOhlcData(ticker, dateFrom, dateTo));
        }

        [HttpGet("DailyPrices/{ticker}/{date}")]
        public async Task<IActionResult> GetDailyPrices(string ticker, string date)
        {
            return Ok(await _stocksService.GetDailyPrices(ticker, date));
        }
    }
}
