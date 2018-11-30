using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task");

            migrationBuilder.AlterColumn<long>(
                name: "TaskCategoryID",
                table: "Task",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task",
                column: "TaskCategoryID",
                principalTable: "TaskCategory",
                principalColumn: "TaskCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task");

            migrationBuilder.AlterColumn<long>(
                name: "TaskCategoryID",
                table: "Task",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryID",
                table: "Task",
                column: "TaskCategoryID",
                principalTable: "TaskCategory",
                principalColumn: "TaskCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
