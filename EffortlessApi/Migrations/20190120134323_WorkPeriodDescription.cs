using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class WorkPeriodDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkPeriods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkPeriods");
        }
    }
}
