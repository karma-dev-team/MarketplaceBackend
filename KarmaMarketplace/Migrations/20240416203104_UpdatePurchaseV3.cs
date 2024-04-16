using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePurchaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Chats_ChatId",
                table: "Purchases");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Purchases",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Chats_ChatId",
                table: "Purchases",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Chats_ChatId",
                table: "Purchases");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Purchases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Chats_ChatId",
                table: "Purchases",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
