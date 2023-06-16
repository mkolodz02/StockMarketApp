namespace StockMarketApp.Shared.Models.DTOs
{
    public class AllOhlcDataDto
    {
        public string Ticker { get; set; } = null!;
        public int QueryCount { get; set; }
        public int ResultsCount { get; set; }
        public bool Adjusted { get; set; }
        public IEnumerable<OhlcData> Results { get; set; } = new List<OhlcData>();
        public string Status { get; set; } = null!;
        public string RequestId { get; set; } = null!;
        public int Count { get; set; }
    }
}
