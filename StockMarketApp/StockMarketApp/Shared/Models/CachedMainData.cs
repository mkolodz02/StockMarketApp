using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApp.Shared.Models
{
    public class CachedMainData
    {
        public int Id { get; set; }
        public string Ticker { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
