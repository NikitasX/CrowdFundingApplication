using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdFundingApplication.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(nullable: true),
                    UserLastName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: false),
                    UserPhone = table.Column<string>(nullable: true),
                    UserVat = table.Column<string>(nullable: true),
                    UserDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserEmail",
                table: "User",
                column: "UserEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
