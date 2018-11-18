using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EffortlessApi.Migrations
{
    public partial class ImplementedAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "UsersJobActive");

            migrationBuilder.DropTable(
                name: "UsersJobInactive");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTemoraryWorkPeriods",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TemporaryWorkPeriods");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "OwnerId");

            migrationBuilder.AddColumn<long>(
                name: "TemporaryWorkPeriodId1",
                table: "UserTemoraryWorkPeriods",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TemporaryWorkPeriodId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Companies",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ParentCompanyId",
                table: "Companies",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Pno",
                table: "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vat",
                table: "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Break",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByOwner",
                table: "Appointments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedByOwnerDate",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ApprovedByUserId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Earnings",
                table: "Appointments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTemoraryWorkPeriods",
                table: "UserTemoraryWorkPeriods",
                columns: new[] { "UserId", "TemporaryWorkPeriodId" });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    AddressId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTemoraryWorkPeriods_TemporaryWorkPeriodId",
                table: "UserTemoraryWorkPeriods",
                column: "TemporaryWorkPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTemoraryWorkPeriods_TemporaryWorkPeriodId1",
                table: "UserTemoraryWorkPeriods",
                column: "TemporaryWorkPeriodId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TemporaryWorkPeriodId",
                table: "Users",
                column: "TemporaryWorkPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ParentCompanyId",
                table: "Companies",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ApprovedByUserId",
                table: "Appointments",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatedByUserId",
                table: "Appointments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TemporaryWorkPeriodId",
                table: "Appointments",
                column: "TemporaryWorkPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AddressId",
                table: "Departments",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_ApprovedByUserId",
                table: "Appointments",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_CreatedByUserId",
                table: "Appointments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Appointments",
                column: "TemporaryWorkPeriodId",
                principalTable: "TemporaryWorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Companies_ParentCompanyId",
                table: "Companies",
                column: "ParentCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Users",
                column: "TemporaryWorkPeriodId",
                principalTable: "TemporaryWorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTemoraryWorkPeriods_TemporaryWorkPeriods_TemporaryWorkP~",
                table: "UserTemoraryWorkPeriods",
                column: "TemporaryWorkPeriodId",
                principalTable: "TemporaryWorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTemoraryWorkPeriods_TemporaryWorkPeriods_TemporaryWork~1",
                table: "UserTemoraryWorkPeriods",
                column: "TemporaryWorkPeriodId1",
                principalTable: "TemporaryWorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTemoraryWorkPeriods_Users_UserId",
                table: "UserTemoraryWorkPeriods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ApprovedByUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_OwnerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Companies_ParentCompanyId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_TemporaryWorkPeriods_TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTemoraryWorkPeriods_TemporaryWorkPeriods_TemporaryWorkP~",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTemoraryWorkPeriods_TemporaryWorkPeriods_TemporaryWork~1",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTemoraryWorkPeriods_Users_UserId",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTemoraryWorkPeriods",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_UserTemoraryWorkPeriods_TemporaryWorkPeriodId",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_UserTemoraryWorkPeriods_TemporaryWorkPeriodId1",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Users_TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ParentCompanyId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ApprovedByUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TemporaryWorkPeriodId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TemporaryWorkPeriodId1",
                table: "UserTemoraryWorkPeriods");

            migrationBuilder.DropColumn(
                name: "TemporaryWorkPeriodId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ParentCompanyId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Pno",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ApprovedByOwner",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApprovedByOwnerDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Earnings",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Appointments",
                newName: "UserId");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "UserTemoraryWorkPeriods",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TemporaryWorkPeriods",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "Break",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTemoraryWorkPeriods",
                table: "UserTemoraryWorkPeriods",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersJobActive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JobId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersJobActive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersJobInactive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JobId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersJobInactive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppointmentId = table.Column<long>(nullable: false),
                    Break = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });
        }
    }
}
