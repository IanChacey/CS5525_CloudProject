using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeepingApp.Data.Migrations
{
    public partial class Initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    ShiftStart = table.Column<DateTime>(nullable: false),
                    ShiftEnd = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
