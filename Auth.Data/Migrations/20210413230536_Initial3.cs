using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username")
                .Annotation("Npgsql:IndexInclude", new[] { "Password" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");
        }
    }
}
