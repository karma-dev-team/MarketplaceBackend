using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Purchases_PurchaseId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Wallets",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Wallets",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Users",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Transactions",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Reviews",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Purchases",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Purchases",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "ProductViews",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProductViews",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Products",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Options",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Options",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Messages",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Messages",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Games",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Games",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Chats",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Chats",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "ChatReads",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ChatReads",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Categories",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Categories",
                newName: "CreatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Reviews",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseId",
                table: "Messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "AutoAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TransactionProviderEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSystems_PaymentProviders_TransactionProviderEntityId",
                        column: x => x.TransactionProviderEntityId,
                        principalTable: "PaymentProviders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSystems_TransactionProviderEntityId",
                table: "PaymentSystems",
                column: "TransactionProviderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Purchases_PurchaseId",
                table: "Messages",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Purchases_PurchaseId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "AutoAnswers");

            migrationBuilder.DropTable(
                name: "PaymentSystems");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Wallets",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Wallets",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Users",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Transactions",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Reviews",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Purchases",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Purchases",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "ProductViews",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ProductViews",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Products",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Options",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Options",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Messages",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Messages",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Games",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Games",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Chats",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Chats",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "ChatReads",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ChatReads",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Categories",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Categories",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Purchases_PurchaseId",
                table: "Messages",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
