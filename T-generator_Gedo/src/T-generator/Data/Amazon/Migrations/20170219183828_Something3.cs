using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class Something3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmazonAccount_AmazonMarketplace_AmazonMarketplaceID",
                table: "AmazonAccount");

            migrationBuilder.DropIndex(
                name: "IX_AmazonAccount_AmazonMarketplaceID",
                table: "AmazonAccount");

            migrationBuilder.DropColumn(
                name: "AmazonMarketplaceID",
                table: "AmazonAccount");

            migrationBuilder.AddColumn<int>(
                name: "AmazonMarketplaceID",
                table: "AmazonDesign",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AmazonDesign_AmazonMarketplaceID",
                table: "AmazonDesign",
                column: "AmazonMarketplaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonDesign_AmazonMarketplace_AmazonMarketplaceID",
                table: "AmazonDesign",
                column: "AmazonMarketplaceID",
                principalTable: "AmazonMarketplace",
                principalColumn: "AmazonMarketplaceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmazonDesign_AmazonMarketplace_AmazonMarketplaceID",
                table: "AmazonDesign");

            migrationBuilder.DropIndex(
                name: "IX_AmazonDesign_AmazonMarketplaceID",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "AmazonMarketplaceID",
                table: "AmazonDesign");

            migrationBuilder.AddColumn<int>(
                name: "AmazonMarketplaceID",
                table: "AmazonAccount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AmazonAccount_AmazonMarketplaceID",
                table: "AmazonAccount",
                column: "AmazonMarketplaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonAccount_AmazonMarketplace_AmazonMarketplaceID",
                table: "AmazonAccount",
                column: "AmazonMarketplaceID",
                principalTable: "AmazonMarketplace",
                principalColumn: "AmazonMarketplaceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
