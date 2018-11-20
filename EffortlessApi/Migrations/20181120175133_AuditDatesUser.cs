using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class AuditDatesUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agreements",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "TemporaryWorkPeriodId",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agreements",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Users_TemporaryWorkPeriodId",
                table: "Users",
                column: "TemporaryWorkPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Users",
                column: "TemporaryWorkPeriodId",
                principalTable: "TemporaryWorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
