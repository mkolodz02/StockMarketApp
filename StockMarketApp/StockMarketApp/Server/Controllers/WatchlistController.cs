using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarketApp.Server.Models;
using StockMarketApp.Server.Services;
using StockMarketApp.Shared.Models;
using System.Security.Claims;

namespace StockMarketApp.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUser user;

        public WatchlistController(IWatchlistService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
            user = _userManager.Users.FirstOrDefault();
        }

        [HttpGet("userWatchlist")]
        public async Task<IActionResult> GetUserWatchlistItems()
        {
            //var user = _userManager.Users.FirstOrDefault();
            return Ok(await _service.GetUserWatchlistItems(user.Email));
        }

        [HttpPost("addToWatchlist")]
        public async Task<IActionResult> AddToWatchlist([FromBody]WatchlistItem item)
        {
            item.UserLogin = user.Email;

            if (await _service.DoesItemExists(item))
            {
                return BadRequest("User already has this company in watchlist");
            }

            await _service.AddToWatchlist(item);
            return Created("api/watchlist", item);
        }


    }
}
