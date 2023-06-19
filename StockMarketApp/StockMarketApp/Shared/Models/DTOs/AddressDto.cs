namespace StockMarketApp.Shared.Models.DTOs
{
    public class AddressDto
    {
        public string Address1 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Postal_code { get; set; } = null!;
    }
}
