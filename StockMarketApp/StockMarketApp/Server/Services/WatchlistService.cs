﻿using Microsoft.EntityFrameworkCore;
using StockMarketApp.Server.Data;
using StockMarketApp.Shared.Models;

namespace StockMarketApp.Server.Services
{
    public interface IWatchlistService
    {
        Task<List<WatchlistItem>> GetUserWatchlistItems(string userLogin);
        Task AddToWatchlist(WatchlistItem watchlistItem);
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
    }
}