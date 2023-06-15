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

        public DbSet<Watchlist> Watchlist { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Watchlist>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.CompanyTicker });
                entity.Property(e => e.UserLogin).IsRequired();
                entity.Property(e => e.CompanyTicker).IsRequired();
            });
        }

    }
}