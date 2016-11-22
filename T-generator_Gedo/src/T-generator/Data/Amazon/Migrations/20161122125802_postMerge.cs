using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tgenerator.Migrations.Amazon
{
    public partial class postMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmazonDesign",
                columns: table => new
                {
                    AmazonDesignID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DesignURL = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonDesign", x => x.AmazonDesignID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    SizeID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => new { x.SizeID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_ProductSizes_AmazonProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "AmazonProduct",
                        principalColumn: "AmazonProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSizes_AmazonSize_SizeID",
                        column: x => x.SizeID,
                        principalTable: "AmazonSize",
                        principalColumn: "AmazonSizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmazonItem",
                columns: table => new
                {
                    AmazonItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmazonBrowseNodeID = table.Column<int>(nullable: false),
                    AmazonCountryID = table.Column<int>(nullable: false),
                    AmazonProductID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: true),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonItem", x => x.AmazonItemID);
                    table.ForeignKey(
                        name: "FK_AmazonItem_AmazonBrowseNode_AmazonBrowseNodeID",
                        column: x => x.AmazonBrowseNodeID,
                        principalTable: "AmazonBrowseNode",
                        principalColumn: "AmazonBrowseNodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmazonItem_AmazonCountry_AmazonCountryID",
                        column: x => x.AmazonCountryID,
                        principalTable: "AmazonCountry",
                        principalColumn: "AmazonCountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmazonItem_AmazonProduct_AmazonProductID",
                        column: x => x.AmazonProductID,
                        principalTable: "AmazonProduct",
                        principalColumn: "AmazonProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "AmazonItemID",
                table: "AmazonKeyword",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmazonItemID",
                table: "AmazonImageURL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmazonItemID",
                table: "AmazonBulletPoint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmazonKeyword_AmazonItemID",
                table: "AmazonKeyword",
                column: "AmazonItemID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonImageURL_AmazonItemID",
                table: "AmazonImageURL",
                column: "AmazonItemID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonBulletPoint_AmazonItemID",
                table: "AmazonBulletPoint",
                column: "AmazonItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductID",
                table: "ProductSizes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_SizeID",
                table: "ProductSizes",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonItem_AmazonBrowseNodeID",
                table: "AmazonItem",
                column: "AmazonBrowseNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonItem_AmazonCountryID",
                table: "AmazonItem",
                column: "AmazonCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonItem_AmazonProductID",
                table: "AmazonItem",
                column: "AmazonProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonBulletPoint_AmazonItem_AmazonItemID",
                table: "AmazonBulletPoint",
                column: "AmazonItemID",
                principalTable: "AmazonItem",
                principalColumn: "AmazonItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonImageURL_AmazonItem_AmazonItemID",
                table: "AmazonImageURL",
                column: "AmazonItemID",
                principalTable: "AmazonItem",
                principalColumn: "AmazonItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonKeyword_AmazonItem_AmazonItemID",
                table: "AmazonKeyword",
                column: "AmazonItemID",
                principalTable: "AmazonItem",
                principalColumn: "AmazonItemID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmazonBulletPoint_AmazonItem_AmazonItemID",
                table: "AmazonBulletPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_AmazonImageURL_AmazonItem_AmazonItemID",
                table: "AmazonImageURL");

            migrationBuilder.DropForeignKey(
                name: "FK_AmazonKeyword_AmazonItem_AmazonItemID",
                table: "AmazonKeyword");

            migrationBuilder.DropIndex(
                name: "IX_AmazonKeyword_AmazonItemID",
                table: "AmazonKeyword");

            migrationBuilder.DropIndex(
                name: "IX_AmazonImageURL_AmazonItemID",
                table: "AmazonImageURL");

            migrationBuilder.DropIndex(
                name: "IX_AmazonBulletPoint_AmazonItemID",
                table: "AmazonBulletPoint");

            migrationBuilder.DropColumn(
                name: "AmazonItemID",
                table: "AmazonKeyword");

            migrationBuilder.DropColumn(
                name: "AmazonItemID",
                table: "AmazonImageURL");

            migrationBuilder.DropColumn(
                name: "AmazonItemID",
                table: "AmazonBulletPoint");

            migrationBuilder.DropTable(
                name: "AmazonDesign");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "AmazonItem");
        }
    }
}
