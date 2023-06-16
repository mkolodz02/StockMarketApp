namespace StockMarketApp.Shared.Models.DTOs
{
    public class DailyPricesDto
    {
        public string Status { get; set; } = null!;
        public string From { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double AfterHours { get; set; }
        public double PreMarket { get; set; }
    }
}
