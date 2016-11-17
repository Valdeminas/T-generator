using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using T_generator.Data;

namespace Tgenerator.Migrations
{
    [DbContext(typeof(AmazonContext))]
    partial class AmazonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonAccount", b =>
                {
                    b.Property<int>("AmazonAccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonAccountID");

                    b.ToTable("AmazonAccount");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonColor", b =>
                {
                    b.Property<int>("AmazonColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonColorID");

                    b.ToTable("AmazonColor");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonCountry", b =>
                {
                    b.Property<int>("AmazonCountryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonCountryID");

                    b.ToTable("AmazonCountry");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonCurrency", b =>
                {
                    b.Property<int>("AmazonCurrencyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency");

                    b.HasKey("AmazonCurrencyID");

                    b.ToTable("AmazonCurrency");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonSize", b =>
                {
                    b.Property<int>("AmazonSizeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonSizeID");

                    b.ToTable("AmazonSize");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonType", b =>
                {
                    b.Property<int>("AmazonTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonTypeID");

                    b.ToTable("AmazonTypes");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonVariation", b =>
                {
                    b.Property<int>("AmazonVariationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonVariationID");

                    b.ToTable("AmazonVariation");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonBrowseNode", b =>
                {
                    b.Property<int>("AmazonBrowseNodeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowseNode");

                    b.HasKey("AmazonBrowseNodeID");

                    b.ToTable("AmazonBrowseNode");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonBulletPoint", b =>
                {
                    b.Property<int>("AmazonBulletPointID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BulletPoint");

                    b.HasKey("AmazonBulletPointID");

                    b.ToTable("AmazonBulletPoint");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonImageURL", b =>
                {
                    b.Property<int>("AmazonImageURLID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageURL");

                    b.HasKey("AmazonImageURLID");

                    b.ToTable("AmazonImageURL");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonKeyword", b =>
                {
                    b.Property<int>("AmazonKeywordID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Keyword");

                    b.HasKey("AmazonKeywordID");

                    b.ToTable("AmazonKeyword");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", b =>
                {
                    b.Property<int>("AmazonMarketplaceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonCurrencyID");

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonMarketplaceID");

                    b.HasIndex("AmazonCurrencyID");

                    b.ToTable("AmazonMarketplace");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", b =>
                {
                    b.Property<int>("AmazonProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonTypeID");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonProductID");

                    b.HasIndex("AmazonTypeID");

                    b.ToTable("AmazonProduct");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.KeywordAssignment", b =>
                {
                    b.Property<int>("KeywordID");

                    b.Property<int>("ProductID");

                    b.Property<int>("Order");

                    b.HasKey("KeywordID", "ProductID", "Order");

                    b.HasIndex("KeywordID");

                    b.HasIndex("ProductID");

                    b.ToTable("KeywordAssignment");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonCurrency", "AmazonCurrency")
                        .WithMany()
                        .HasForeignKey("AmazonCurrencyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonType", "AmazonType")
                        .WithMany()
                        .HasForeignKey("AmazonTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.KeywordAssignment", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Dump.AmazonKeyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "Product")
                        .WithMany("Keywords")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
