using Microsoft.EntityFrameworkCore.Migrations;

namespace agilesheel.Migrations
{
    public partial class testTicket1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Shows_ShowId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ShowId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Shows_ShowId",
                table: "Tickets",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Shows_ShowId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ShowId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Shows_ShowId",
                table: "Tickets",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
