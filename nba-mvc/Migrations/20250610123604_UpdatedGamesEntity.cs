using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGamesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Game");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Game_TeamId",
                table: "Game",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Team_TeamId",
                table: "Game",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Team_TeamId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_TeamId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
