using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToCurrent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Images_PhotoId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Chats_PhotoId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Chats");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProviderId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageId",
                table: "Users",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ImageId",
                table: "Chats",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Images_ImageId",
                table: "Chats",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions",
                column: "ProviderId",
                principalTable: "TransactionProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Images_ImageId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ImageId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Chats");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProviderId",
                table: "Transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Chats_PhotoId",
                table: "Chats",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Images_PhotoId",
                table: "Chats",
                column: "PhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions",
                column: "ProviderId",
                principalTable: "TransactionProviders",
                principalColumn: "Id");
        }
    }
}
