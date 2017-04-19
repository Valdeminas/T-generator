using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmazonAccountID",
                table: "AmazonProduct",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmazonAccountID",
                table: "AmazonDesign",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmazonMarketplaceID",
                table: "AmazonAccount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AmazonProduct_AmazonAccountID",
                table: "AmazonProduct",
                column: "AmazonAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonDesign_AmazonAccountID",
                table: "AmazonDesign",
                column: "AmazonAccountID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonDesign_AmazonAccount_AmazonAccountID",
                table: "AmazonDesign",
                column: "AmazonAccountID",
                principalTable: "AmazonAccount",
                principalColumn: "AmazonAccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonProduct_AmazonAccount_AmazonAccountID",
                table: "AmazonProduct",
                column: "AmazonAccountID",
                principalTable: "AmazonAccount",
                principalColumn: "AmazonAccountID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmazonAccount_AmazonMarketplace_AmazonMarketplaceID",
                table: "AmazonAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_AmazonDesign_AmazonAccount_AmazonAccountID",
                table: "AmazonDesign");

            migrationBuilder.DropForeignKey(
                name: "FK_AmazonProduct_AmazonAccount_AmazonAccountID",
                table: "AmazonProduct");

            migrationBuilder.DropIndex(
                name: "IX_AmazonProduct_AmazonAccountID",
                table: "AmazonProduct");

            migrationBuilder.DropIndex(
                name: "IX_AmazonDesign_AmazonAccountID",
                table: "AmazonDesign");

            migrationBuilder.DropIndex(
                name: "IX_AmazonAccount_AmazonMarketplaceID",
                table: "AmazonAccount");

            migrationBuilder.DropColumn(
                name: "AmazonAccountID",
                table: "AmazonProduct");

            migrationBuilder.DropColumn(
                name: "AmazonAccountID",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "AmazonMarketplaceID",
                table: "AmazonAccount");
        }
    }
}
