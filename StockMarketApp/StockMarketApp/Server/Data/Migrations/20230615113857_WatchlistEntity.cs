using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarketApp.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class WatchlistEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    UserLogin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyTicker = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => new { x.UserLogin, x.CompanyTicker });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watchlist");
        }
    }
}
