using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tgenerator.Migrations.Amazon
{
    public partial class something2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "AmazonDesign");

            migrationBuilder.CreateTable(
                name: "AmazonCategory",
                columns: table => new
                {
                    AmazonCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonCategory", x => x.AmazonCategoryID);
                });

            migrationBuilder.AddColumn<int>(
                name: "AmazonCategoryID",
                table: "AmazonDesign",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SearchTags1",
                table: "AmazonDesign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchTags2",
                table: "AmazonDesign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchTags3",
                table: "AmazonDesign",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmazonDesign_AmazonCategoryID",
                table: "AmazonDesign",
                column: "AmazonCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_AmazonDesign_AmazonCategory_AmazonCategoryID",
                table: "AmazonDesign",
                column: "AmazonCategoryID",
                principalTable: "AmazonCategory",
                principalColumn: "AmazonCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmazonDesign_AmazonCategory_AmazonCategoryID",
                table: "AmazonDesign");

            migrationBuilder.DropIndex(
                name: "IX_AmazonDesign_AmazonCategoryID",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "AmazonCategoryID",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "SearchTags1",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "SearchTags2",
                table: "AmazonDesign");

            migrationBuilder.DropColumn(
                name: "SearchTags3",
                table: "AmazonDesign");

            migrationBuilder.DropTable(
                name: "AmazonCategory");

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "AmazonDesign",
                nullable: true);
        }
    }
}
