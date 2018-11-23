using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TaskCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategory_UserId",
                table: "TaskCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategory_User_UserId",
                table: "TaskCategory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategory_User_UserId",
                table: "TaskCategory");

            migrationBuilder.DropIndex(
                name: "IX_TaskCategory_UserId",
                table: "TaskCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskCategory");
        }
    }
}
