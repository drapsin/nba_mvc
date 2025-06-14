using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionEvent_Team_TeamId",
                table: "ActionEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Arena_ArenaId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "ArenaId",
                table: "Game",
                newName: "TeamsId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_ArenaId",
                table: "Game",
                newName: "IX_Game_TeamsId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId1",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId1",
                table: "Player",
                column: "TeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_Game_LocationId",
                table: "Game",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionEvent_Team_TeamId",
                table: "ActionEvent",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Arena_LocationId",
                table: "Game",
                column: "LocationId",
                principalTable: "Arena",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Team_TeamsId",
                table: "Game",
                column: "TeamsId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId1",
                table: "Player",
                column: "TeamId1",
                principalTable: "Team",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionEvent_Team_TeamId",
                table: "ActionEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Arena_LocationId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Team_TeamsId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId1",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TeamId1",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Game_LocationId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "Game",
                newName: "ArenaId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_TeamsId",
                table: "Game",
                newName: "IX_Game_ArenaId");

            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionEvent_Team_TeamId",
                table: "ActionEvent",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Arena_ArenaId",
                table: "Game",
                column: "ArenaId",
                principalTable: "Arena",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
