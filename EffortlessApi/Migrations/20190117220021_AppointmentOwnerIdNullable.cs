using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class AppointmentOwnerIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
