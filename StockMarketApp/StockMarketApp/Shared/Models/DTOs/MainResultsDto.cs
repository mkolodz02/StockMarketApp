namespace StockMarketApp.Shared.Models.DTOs
{
    public class MainResultsDto
    {
        public string Ticker { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Market { get; set; } = null!;
        public string Locale { get; set; } = null!;
        public string Primary_exchange { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool Active { get; set; }
        public string Current_name { get; set; } = null!;
        public string Cik { get; set; } = null!;
        public string Composite_figi { get; set; } = null!;
        public string Share_class_figi { get; set; } = null!;
        public double Market_cap { get; set; }
        public string Phone_number { get; set; } = null!;
        public AddressDto Address { get; set; }
        public string Description { get; set; } = null!;
        public string Sic_code { get; set; } = null!;
        public string Sic_description { get; set; } = null!;
        public string Ticker_root { get; set; } = null!;
        public string Homepage_url { get; set; } = null!;
        public long Total_employees { get; set; }
        public string List_date { get; set;} = null!;
        public BrandingDto Branding { get; set; }
    }
}
