using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LeaderBoardService.Migrations
{
    public partial class SesionDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSession",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: true),
                    startTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Y1L = table.Column<string>(type: "text", nullable: true),
                    Y1T = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Y1I = table.Column<string>(type: "text", nullable: true),
                    Y2L = table.Column<string>(type: "text", nullable: true),
                    Y2I = table.Column<string>(type: "text", nullable: true),
                    Y2T = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Y3L = table.Column<string>(type: "text", nullable: true),
                    Y3I = table.Column<string>(type: "text", nullable: true),
                    Y3T = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    LeaderBoardID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSession", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameSession_LeaderBoard_LeaderBoardID",
                        column: x => x.LeaderBoardID,
                        principalTable: "LeaderBoard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSession_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_LeaderBoardID",
                table: "GameSession",
                column: "LeaderBoardID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_UserID",
                table: "GameSession",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSession");
        }
    }
}
