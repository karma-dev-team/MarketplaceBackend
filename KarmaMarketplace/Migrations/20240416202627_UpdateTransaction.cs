using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "Transactions",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
