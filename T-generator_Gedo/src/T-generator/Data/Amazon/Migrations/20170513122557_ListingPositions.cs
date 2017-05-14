using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class ListingPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "AmazonListing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Right",
                table: "AmazonListing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Top",
                table: "AmazonListing",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Left",
                table: "AmazonListing");

            migrationBuilder.DropColumn(
                name: "Right",
                table: "AmazonListing");

            migrationBuilder.DropColumn(
                name: "Top",
                table: "AmazonListing");
        }
    }
}
