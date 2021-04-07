using Microsoft.EntityFrameworkCore.Migrations;

namespace agilesheel.Migrations
{
    public partial class addUser_LostAndFound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LostAndFound",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LostAndFound_UserId",
                table: "LostAndFound",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LostAndFound_AspNetUsers_UserId",
                table: "LostAndFound",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LostAndFound_AspNetUsers_UserId",
                table: "LostAndFound");

            migrationBuilder.DropIndex(
                name: "IX_LostAndFound_UserId",
                table: "LostAndFound");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LostAndFound",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
