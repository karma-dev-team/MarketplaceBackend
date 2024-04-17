using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarmaMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class MaaanChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ChatEntityUserEntity",
                columns: table => new
                {
                    ChatsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatEntityUserEntity", x => new { x.ChatsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ChatEntityUserEntity_Chats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatEntityUserEntity_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntityUserEntity_ParticipantsId",
                table: "ChatEntityUserEntity",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatEntityUserEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatEntityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users",
                column: "ChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users",
                column: "ChatEntityId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
