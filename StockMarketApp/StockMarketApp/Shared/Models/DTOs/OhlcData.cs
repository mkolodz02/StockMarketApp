using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApp.Shared.Models.DTOs
{
    public class OhlcData
    {
        public long V { get; set; }
        public double VW { get; set; }
        public double O { get; set; }
        public double C { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public long T { get; set; }
        public int N { get; set; }
    }
}
