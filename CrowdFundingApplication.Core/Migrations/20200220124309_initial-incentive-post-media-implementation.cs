using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdFundingApplication.Core.Migrations
{
    public partial class initialincentivepostmediaimplementation : Migration
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
                    UserLastName = table.Column<string>(maxLength: 255, nullable: false),
                    UserEmail = table.Column<string>(maxLength: 255, nullable: false),
                    UserPhone = table.Column<string>(nullable: true),
                    UserVat = table.Column<string>(nullable: true),
                    UserDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    ProjectTitle = table.Column<string>(maxLength: 255, nullable: false),
                    ProjectDescription = table.Column<string>(nullable: true),
                    ProjectFinancialGoal = table.Column<decimal>(maxLength: 20, nullable: false),
                    ProjectCapitalAcquired = table.Column<decimal>(nullable: false),
                    ProjectCategory = table.Column<int>(nullable: false),
                    ProjectDateCreated = table.Column<DateTimeOffset>(nullable: false),
                    ProjectDateExpiring = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incentive",
                columns: table => new
                {
                    IncentiveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: true),
                    IncentiveTitle = table.Column<string>(maxLength: 255, nullable: false),
                    IncentiveDescription = table.Column<string>(nullable: true),
                    IncentivePrice = table.Column<decimal>(maxLength: 20, nullable: false),
                    IncentiveReward = table.Column<string>(nullable: true),
                    IncentiveDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incentive", x => x.IncentiveId);
                    table.ForeignKey(
                        name: "FK_Incentive_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: true),
                    MediaType = table.Column<int>(nullable: false),
                    MediaURL = table.Column<string>(nullable: false),
                    MediaDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_Media_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    PostTitle = table.Column<string>(maxLength: 255, nullable: false),
                    PostExcerpt = table.Column<string>(nullable: true),
                    PostDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BackedIncentives",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    IncentiveId = table.Column<int>(nullable: false),
                    BackedIncentiveDateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackedIncentives", x => new { x.UserId, x.IncentiveId });
                    table.ForeignKey(
                        name: "FK_BackedIncentives_Incentive_IncentiveId",
                        column: x => x.IncentiveId,
                        principalTable: "Incentive",
                        principalColumn: "IncentiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackedIncentives_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackedIncentives_IncentiveId",
                table: "BackedIncentives",
                column: "IncentiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Incentive_ProjectId",
                table: "Incentive",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProjectId",
                table: "Media",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_ProjectId",
                table: "Post",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserEmail",
                table: "User",
                column: "UserEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackedIncentives");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Incentive");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
