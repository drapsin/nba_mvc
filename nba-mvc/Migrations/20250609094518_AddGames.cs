using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sponsor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    GameTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionEvent_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionEvent_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionEvent_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEvent_GameId",
                table: "ActionEvent",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEvent_PlayerId",
                table: "ActionEvent",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEvent_TeamId",
                table: "ActionEvent",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "ActionEvent");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Player_GameId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Player");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
