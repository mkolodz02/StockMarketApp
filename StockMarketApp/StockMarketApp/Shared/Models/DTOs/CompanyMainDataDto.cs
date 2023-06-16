namespace StockMarketApp.Shared.Models.DTOs
{
    public class CompanyMainDataDto
    {
        public string Ticker { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
