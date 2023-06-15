using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using StockMarketApp.Server.Models;
using StockMarketApp.Server.Services;
using System.Security.Claims;

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

        [HttpGet]
        public async Task<IActionResult> GetStockInfo()
        {
            return Ok(await _stocksService.GetStockInfo("TSLA"));
        }

        [HttpGet("getStocksList")]
        public async Task<IActionResult> GetStockList()
        {
            return Ok(await _stocksService.GetStockList("TSL"));
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            //var userName = HttpContext.User.Identity;
            return Ok();
        }

        [HttpGet("getOhlcData")]
        public async Task<IActionResult> GetOhlcData()
        {
            return Ok(await _stocksService.GetOhlcData("2023-01-09", "2023-01-14", "TSLA"));
        }

        [HttpGet("getDailyPrices")]
        public async Task<IActionResult> GetDailyPrices()
        {
            return Ok(await _stocksService.GetDailyPrices("TSLA", "2023-03-02"));
        }
    }
}
