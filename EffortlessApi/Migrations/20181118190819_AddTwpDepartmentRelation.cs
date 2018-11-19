using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class AddTwpDepartmentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakIsPaid",
                table: "TemporaryWorkPeriods");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "TemporaryWorkPeriods");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "TemporaryWorkPeriods");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "TemporaryWorkPeriods",
                newName: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryWorkPeriods_DepartmentId",
                table: "TemporaryWorkPeriods",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryWorkPeriods_Departments_DepartmentId",
                table: "TemporaryWorkPeriods",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryWorkPeriods_Departments_DepartmentId",
                table: "TemporaryWorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryWorkPeriods_DepartmentId",
                table: "TemporaryWorkPeriods");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "TemporaryWorkPeriods",
                newName: "JobId");

            migrationBuilder.AddColumn<bool>(
                name: "BreakIsPaid",
                table: "TemporaryWorkPeriods",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Salary",
                table: "TemporaryWorkPeriods",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "TemporaryWorkPeriods",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
