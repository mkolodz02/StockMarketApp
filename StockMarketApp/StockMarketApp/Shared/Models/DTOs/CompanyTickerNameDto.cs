using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApp.Shared.Models.DTOs
{
    public class CompanyTickerNameDto
    {
        public string Ticker { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Market { get; set; } = null!;
        public string Locale { get; set; } = null!;
        public string Primary_Exchange { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Active { get; set; } = null!;
        public string Currency_Name { get; set; } = null!;
        public string Composite_figi { get; set; } = null!;
        public string Share_class_figi { get; set; } = null!;
        public string Last_updated_utc { get; set; } = null!;
    }
}
