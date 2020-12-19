using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderBoardService.Migrations
{
    public partial class blabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "LeaderBoard",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "LeaderBoard");
        }
    }
}
