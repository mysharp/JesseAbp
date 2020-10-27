using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Initialv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Guarantee",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Guarantee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Guarantee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Applier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "IdNo",
                table: "Applier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Applier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Applier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Guarantee");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Guarantee");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Guarantee");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Applier");

            migrationBuilder.DropColumn(
                name: "IdNo",
                table: "Applier");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Applier");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Applier");
        }
    }
}
