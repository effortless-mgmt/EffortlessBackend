using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EffortlessApi.Migrations
{
    public partial class add_all_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Road = table.Column<string>(nullable: false),
                    No = table.Column<int>(nullable: false),
                    Floor = table.Column<string>(nullable: true),
                    Side = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    ZipCode = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false),
                    Break = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    TemporaryWorkPeriodId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePrivileges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<long>(nullable: false),
                    PrivilegeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryWorkPeriods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    JobId = table.Column<long>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false),
                    UnitPrice = table.Column<float>(nullable: false),
                    Salary = table.Column<float>(nullable: false),
                    BreakIsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryWorkPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersJobActive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    JobId = table.Column<long>(nullable: false)
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
                    UserId = table.Column<long>(nullable: false),
                    JobId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersJobInactive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTemoraryWorkPeriods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    TemporaryWorkPeriodId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTemoraryWorkPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    AppointmentId = table.Column<long>(nullable: false),
                    Break = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "RolePrivileges");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TemporaryWorkPeriods");

            migrationBuilder.DropTable(
                name: "UsersJobActive");

            migrationBuilder.DropTable(
                name: "UsersJobInactive");

            migrationBuilder.DropTable(
                name: "UserTemoraryWorkPeriods");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Hours",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyId = table.Column<long>(nullable: true),
                    OwnerId = table.Column<long>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hours_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hours_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hours_CompanyId",
                table: "Hours",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_OwnerId",
                table: "Hours",
                column: "OwnerId");
        }
    }
}
