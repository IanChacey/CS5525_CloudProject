using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeepingApp.Data.Migrations
{
    public partial class revision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Shift");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    HourlyWage = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Shift",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Shift",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Shift",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
