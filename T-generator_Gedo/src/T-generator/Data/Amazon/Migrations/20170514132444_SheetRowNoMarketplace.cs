using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class SheetRowNoMarketplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SheetNumber",
                table: "AmazonMarketplace",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartingRow",
                table: "AmazonMarketplace",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheetNumber",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "StartingRow",
                table: "AmazonMarketplace");
        }
    }
}
