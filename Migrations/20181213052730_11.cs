using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "isImportant",
                table: "Task",
                newName: "isDone");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Task",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TaskDesription",
                table: "Task",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "TaskCategoryID",
                table: "Task",
                newName: "CategoryTaskCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Task_TaskCategoryID",
                table: "Task",
                newName: "IX_Task_CategoryTaskCategoryID");

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Task",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpiredAt",
                table: "Task",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_CategoryTaskCategoryID",
                table: "Task",
                column: "CategoryTaskCategoryID",
                principalTable: "TaskCategory",
                principalColumn: "TaskCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_CategoryTaskCategoryID",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "isDone",
                table: "Task",
                newName: "isImportant");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Task",
                newName: "TaskName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Task",
                newName: "TaskDesription");

            migrationBuilder.RenameColumn(
                name: "CategoryTaskCategoryID",
                table: "Task",
                newName: "TaskCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Task_CategoryTaskCategoryID",
                table: "Task",
                newName: "IX_Task_TaskCategoryID");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task",
                column: "TaskCategoryID",
                principalTable: "TaskCategory",
                principalColumn: "TaskCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
