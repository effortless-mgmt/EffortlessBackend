using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class RefactorRoadToStreet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Road",
                table: "Addresses",
                newName: "Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Addresses",
                newName: "Road");
        }
    }
}
