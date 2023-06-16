using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarketApp.Server.Models;
using StockMarketApp.Server.Services;
using StockMarketApp.Shared.Models;
using System.Security.Claims;

namespace StockMarketApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public WatchlistController(IWatchlistService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;

        }

        [HttpGet("userWatchlist")]
        public async Task<IActionResult> GetUserWatchlistItems()
        {
            var user = _userManager.Users.FirstOrDefault();
            return Ok(await _service.GetUserWatchlistItems(user.Email));
        }

        [HttpPost("addToWatchlist")]
        public async Task<IActionResult> AddToWatchlist(WatchlistItem item)
        {
            await _service.AddToWatchlist(item);
            return Created("api/watchlist", "New item added to watchlist");
        }


    }
}
