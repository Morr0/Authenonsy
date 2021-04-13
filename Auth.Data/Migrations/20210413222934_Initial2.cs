using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "text", nullable: true),
                    RedirectUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FirstParty = table.Column<bool>(type: "boolean", nullable: false),
                    Scopes = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "UserApplicationAccess",
                columns: table => new
                {
                    ApplicationClientId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Scopes = table.Column<List<string>>(type: "text[]", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplicationAccess", x => new { x.UserId, x.ApplicationClientId });
                });

            migrationBuilder.CreateTable(
                name: "UserApplicationCodeRequest",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    ApplicationAccessUserId = table.Column<string>(type: "text", nullable: true),
                    ApplicationAccessApplicationClientId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplicationCodeRequest", x => x.Code);
                    table.ForeignKey(
                        name: "FK_UserApplicationCodeRequest_UserApplicationAccess_Applicatio~",
                        columns: x => new { x.ApplicationAccessUserId, x.ApplicationAccessApplicationClientId },
                        principalTable: "UserApplicationAccess",
                        principalColumns: new[] { "UserId", "ApplicationClientId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserApplicationSession",
                columns: table => new
                {
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplicationAccessUserId = table.Column<string>(type: "text", nullable: true),
                    ApplicationAccessApplicationClientId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplicationSession", x => x.AccessToken);
                    table.ForeignKey(
                        name: "FK_UserApplicationSession_UserApplicationAccess_ApplicationAcc~",
                        columns: x => new { x.ApplicationAccessUserId, x.ApplicationAccessApplicationClientId },
                        principalTable: "UserApplicationAccess",
                        principalColumns: new[] { "UserId", "ApplicationClientId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationCodeRequest_ApplicationAccessUserId_Applicat~",
                table: "UserApplicationCodeRequest",
                columns: new[] { "ApplicationAccessUserId", "ApplicationAccessApplicationClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationSession_ApplicationAccessUserId_ApplicationA~",
                table: "UserApplicationSession",
                columns: new[] { "ApplicationAccessUserId", "ApplicationAccessApplicationClientId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "UserApplicationCodeRequest");

            migrationBuilder.DropTable(
                name: "UserApplicationSession");

            migrationBuilder.DropTable(
                name: "UserApplicationAccess");
        }
    }
}
