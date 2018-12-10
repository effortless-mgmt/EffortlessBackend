using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class PnoFromCompanyToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pno",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "Pno",
                table: "Departments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pno",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "Pno",
                table: "Companies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
