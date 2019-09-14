#pragma warning disable 1591
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostIncomeCalculator.Migrations
{
    public partial class Dateforcostandincome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Incomes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Costs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Costs");
        }
    }
}
