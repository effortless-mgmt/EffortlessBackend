﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EffortlessApi.Migrations
{
    public partial class RefactorWorkPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ApprovedByUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ApprovedByUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CreatedByUserId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Stop",
                table: "WorkPeriods",
                newName: "LastAppointment");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "WorkPeriods",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "AgreementId",
                table: "WorkPeriods",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkPeriods",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkPeriodId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ApprovedById",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPeriods_AgreementId",
                table: "WorkPeriods",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkPeriodId",
                table: "Users",
                column: "WorkPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ParentCompanyId",
                table: "Companies",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ApprovedById",
                table: "Appointments",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatedById",
                table: "Appointments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_ApprovedById",
                table: "Appointments",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_CreatedById",
                table: "Appointments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Companies_ParentCompanyId",
                table: "Companies",
                column: "ParentCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WorkPeriods_WorkPeriodId",
                table: "Users",
                column: "WorkPeriodId",
                principalTable: "WorkPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPeriods_Agreements_AgreementId",
                table: "WorkPeriods",
                column: "AgreementId",
                principalTable: "Agreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ApprovedById",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatedById",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Companies_ParentCompanyId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_WorkPeriods_WorkPeriodId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPeriods_Agreements_AgreementId",
                table: "WorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_WorkPeriods_AgreementId",
                table: "WorkPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkPeriodId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ParentCompanyId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ApprovedById",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CreatedById",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "WorkPeriods");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "WorkPeriods");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkPeriods");

            migrationBuilder.DropColumn(
                name: "WorkPeriodId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "LastAppointment",
                table: "WorkPeriods",
                newName: "Stop");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ApprovedByUserId",
                table: "Appointments",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatedByUserId",
                table: "Appointments",
                column: "CreatedByUserId");

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
        }
    }
}
