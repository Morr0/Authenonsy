using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Data.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserApplicationAccess");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserApplicationSession",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserApplicationSession");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserApplicationAccess",
                type: "text",
                nullable: true);
        }
    }
}
