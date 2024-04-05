using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class BigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSystems_PaymentProviders_TransactionProviderEntityId",
                table: "PaymentSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentProviders",
                table: "PaymentProviders");

            migrationBuilder.RenameTable(
                name: "PaymentProviders",
                newName: "TransactionProviders");

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "TransactionProviders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionProviders",
                table: "TransactionProviders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProviderId",
                table: "Transactions",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSystems_TransactionProviders_TransactionProviderEnti~",
                table: "PaymentSystems",
                column: "TransactionProviderEntityId",
                principalTable: "TransactionProviders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions",
                column: "ProviderId",
                principalTable: "TransactionProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSystems_TransactionProviders_TransactionProviderEnti~",
                table: "PaymentSystems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionProviders_ProviderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ProviderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionProviders",
                table: "TransactionProviders");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "TransactionProviders");

            migrationBuilder.RenameTable(
                name: "TransactionProviders",
                newName: "PaymentProviders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentProviders",
                table: "PaymentProviders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSystems_PaymentProviders_TransactionProviderEntityId",
                table: "PaymentSystems",
                column: "TransactionProviderEntityId",
                principalTable: "PaymentProviders",
                principalColumn: "Id");
        }
    }
}
