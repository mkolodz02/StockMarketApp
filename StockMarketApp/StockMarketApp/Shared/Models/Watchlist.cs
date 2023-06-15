using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApp.Shared.Models
{
    public class Watchlist
    {
        public string UserLogin { get; set; } = null!;
        public string CompanyTicker { get; set; } = null!;
    }
}
