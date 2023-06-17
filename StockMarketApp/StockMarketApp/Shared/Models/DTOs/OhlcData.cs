namespace StockMarketApp.Shared.Models.DTOs
{
    public class OhlcData
    {
        public DateTime Date { get; set; }
        public double O { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public double VW { get; set; }
    }
}
