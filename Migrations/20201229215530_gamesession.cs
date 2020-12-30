using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderBoardService.Migrations
{
    public partial class gamesession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSession_LeaderBoard_LeaderBoardID",
                table: "GameSession");

            migrationBuilder.DropIndex(
                name: "IX_GameSession_LeaderBoardID",
                table: "GameSession");

            migrationBuilder.DropColumn(
                name: "LeaderBoardID",
                table: "GameSession");

            migrationBuilder.AddColumn<int>(
                name: "sessionID",
                table: "LeaderBoard",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderBoard_sessionID",
                table: "LeaderBoard",
                column: "sessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderBoard_GameSession_sessionID",
                table: "LeaderBoard",
                column: "sessionID",
                principalTable: "GameSession",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderBoard_GameSession_sessionID",
                table: "LeaderBoard");

            migrationBuilder.DropIndex(
                name: "IX_LeaderBoard_sessionID",
                table: "LeaderBoard");

            migrationBuilder.DropColumn(
                name: "sessionID",
                table: "LeaderBoard");

            migrationBuilder.AddColumn<int>(
                name: "LeaderBoardID",
                table: "GameSession",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_LeaderBoardID",
                table: "GameSession",
                column: "LeaderBoardID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSession_LeaderBoard_LeaderBoardID",
                table: "GameSession",
                column: "LeaderBoardID",
                principalTable: "LeaderBoard",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
