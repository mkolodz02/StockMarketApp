using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.Events;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockMarketApp.Server.Models;
using StockMarketApp.Shared.Models;

namespace StockMarketApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        public DbSet<WatchlistItem> Watchlist { get; set; }
        public DbSet<CachedMainData> MainCompanyData { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WatchlistItem>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.CompanyTicker });
                entity.Property(e => e.UserLogin).IsRequired();
                entity.Property(e => e.CompanyTicker).IsRequired();
            });

            builder.Entity<CachedMainData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ticker).IsRequired();
                entity.Property(e => e.Logo).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.City).IsRequired();
            });

        }

    }
}