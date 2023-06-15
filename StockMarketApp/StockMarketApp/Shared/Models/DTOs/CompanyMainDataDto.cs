using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApp.Shared.Models.DTOs
{
    public class CompanyMainDataDto
    {
        public string Ticker { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
