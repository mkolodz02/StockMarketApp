namespace StockMarketApp.Shared.Models.DTOs
{
    public class CompanyMainDataDto
    {
        public string Request_id { get; set; } = null!;
        public MainResultsDto Results { get; set; }
        public string Status { get; set; } = null!;
    }
}
