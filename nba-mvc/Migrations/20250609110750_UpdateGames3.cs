using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGames3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameLocation",
                table: "Game");

            migrationBuilder.AddColumn<Guid>(
                name: "ArenaId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Game_ArenaId",
                table: "Game",
                column: "ArenaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Arena_ArenaId",
                table: "Game",
                column: "ArenaId",
                principalTable: "Arena",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Arena_ArenaId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_ArenaId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "GameLocation",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
