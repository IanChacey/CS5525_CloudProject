using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeepingApp.Data.Migrations
{
    public partial class fix_attempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shift");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
