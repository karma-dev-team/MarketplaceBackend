using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "TransactionProviders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProviders_LogoId",
                table: "TransactionProviders",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionProviders_Files_LogoId",
                table: "TransactionProviders",
                column: "LogoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionProviders_Files_LogoId",
                table: "TransactionProviders");

            migrationBuilder.DropIndex(
                name: "IX_TransactionProviders_LogoId",
                table: "TransactionProviders");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "TransactionProviders");
        }
    }
}
