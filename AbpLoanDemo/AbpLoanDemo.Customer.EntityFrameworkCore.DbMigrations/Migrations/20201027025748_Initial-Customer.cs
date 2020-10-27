using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class InitialCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customer",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IdNo = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Customer", x => x.Id); });

            migrationBuilder.CreateTable(
                "Linkman",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IdNo = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linkman", x => x.Id);
                    table.ForeignKey(
                        "FK_Linkman_Customer_CustomerId",
                        x => x.CustomerId,
                        "Customer",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Linkman_CustomerId",
                "Linkman",
                "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Linkman");

            migrationBuilder.DropTable(
                "Customer");
        }
    }
}