using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tgenerator.Migrations.Amazon
{
    public partial class Listing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmazonListing",
                columns: table => new
                {
                    AmazonListingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmazonDesignID = table.Column<int>(nullable: true),
                    AmazonProductColorID = table.Column<int>(nullable: true),
                    AmazonProductID = table.Column<int>(nullable: true),
                    DesignURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonListing", x => x.AmazonListingID);
                    table.ForeignKey(
                        name: "FK_AmazonListing_AmazonDesign_AmazonDesignID",
                        column: x => x.AmazonDesignID,
                        principalTable: "AmazonDesign",
                        principalColumn: "AmazonDesignID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AmazonListing_AmazonProductColor_AmazonProductColorID",
                        column: x => x.AmazonProductColorID,
                        principalTable: "AmazonProductColor",
                        principalColumn: "AmazonProductColorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AmazonListing_AmazonProduct_AmazonProductID",
                        column: x => x.AmazonProductID,
                        principalTable: "AmazonProduct",
                        principalColumn: "AmazonProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmazonListing_AmazonDesignID",
                table: "AmazonListing",
                column: "AmazonDesignID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonListing_AmazonProductColorID",
                table: "AmazonListing",
                column: "AmazonProductColorID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonListing_AmazonProductID",
                table: "AmazonListing",
                column: "AmazonProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmazonListing");
        }
    }
}
