using Microsoft.EntityFrameworkCore;
using StockMarketApp.Server.Data;
using StockMarketApp.Shared.Models;

namespace StockMarketApp.Server.Services
{
    public interface IWatchlistService
    {
        Task<List<WatchlistItem>> GetUserWatchlistItems(string userLogin);
        Task AddToWatchlist(WatchlistItem watchlistItem);
        Task<bool> DoesItemExists(WatchlistItem watchlistItem);
        Task RemoveFromWatchlist(WatchlistItem item);
    }

    public class WatchlistService : IWatchlistService
    {
        private readonly ApplicationDbContext _context;

        public WatchlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToWatchlist(WatchlistItem watchlistItem)
        {          
                await _context.AddAsync(watchlistItem);
                await _context.SaveChangesAsync();
        }

        public async Task<List<WatchlistItem>> GetUserWatchlistItems(string userLogin)
        {
            var watchlistItems = await _context.Watchlist.Where(e => e.UserLogin == userLogin).ToListAsync();
            return watchlistItems;
        }

        public async Task<bool> DoesItemExists(WatchlistItem watchlistItem)
        {
            var item = await _context.Watchlist.FirstOrDefaultAsync(i => i.UserLogin.Equals(watchlistItem.UserLogin) &&
                i.CompanyTicker.Equals(watchlistItem.CompanyTicker)); // Comparator?

            return item != null;    

        }

        public async Task RemoveFromWatchlist(WatchlistItem item)
        {
            var itemToDelete = _context.Watchlist.FirstOrDefault(i => i.UserLogin.Equals(item.UserLogin) && i.CompanyTicker.Equals(item.CompanyTicker));
            _context.Watchlist.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
