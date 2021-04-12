using Microsoft.EntityFrameworkCore.Migrations;

namespace agilesheel.Migrations
{
    public partial class verkoperId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerkoperId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerkoperId",
                table: "Tickets");
        }
    }
}
