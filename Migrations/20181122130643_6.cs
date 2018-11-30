using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TaskCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isImportant",
                table: "Task",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    TaskId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => new { x.UserId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_UserTasks_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTasks_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_TaskId",
                table: "UserTasks",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TaskCategory");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "isImportant",
                table: "Task");
        }
    }
}
