using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using T_generator.Models.Amazon.Data.JoinTables;
using T_generator.Models.Amazon.Objects.Item;
using T_generator.Models.Amazon.Objects.Item.Children;

namespace T_generator.Data
{
    public class AmazonContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<AmazonBrowseNode>().ToTable("AmazonBrowseNode");
            modelBuilder.Entity<AmazonAccount>().ToTable("AmazonAccount");
            modelBuilder.Entity<AmazonColor>().ToTable("AmazonColor");
            modelBuilder.Entity<AmazonCountry>().ToTable("AmazonCountry");
            modelBuilder.Entity<AmazonCountry>().ToTable("AmazonCountry");
            modelBuilder.Entity<AmazonCurrency>().ToTable("AmazonCurrency");
            modelBuilder.Entity<AmazonSize>().ToTable("AmazonSize");
            modelBuilder.Entity<AmazonVariation>().ToTable("AmazonVariation");
            modelBuilder.Entity<AmazonBulletPoint>().ToTable("AmazonBulletPoint");
            modelBuilder.Entity<AmazonImageURL>().ToTable("AmazonImageURL");
            modelBuilder.Entity<AmazonKeyword>().ToTable("AmazonKeyword");
            modelBuilder.Entity<AmazonCategory>().ToTable("AmazonCategory");
            modelBuilder.Entity<AmazonProduct>().ToTable("AmazonProduct");
            modelBuilder.Entity<AmazonProductColor>().ToTable("AmazonProductColor");
            modelBuilder.Entity<AmazonMarketplace>().ToTable("AmazonMarketplace");
            modelBuilder.Entity<AmazonDesign>().ToTable("AmazonDesign");
            modelBuilder.Entity<KeywordAssignment>().ToTable("KeywordAssignment");
            modelBuilder.Entity<ProductSizes>().ToTable("ProductSizes");
            modelBuilder.Entity<AmazonItem>().ToTable("AmazonItem");
            modelBuilder.Entity<AmazonItemSingle>().ToTable("AmazonItemSingle");
            modelBuilder.Entity<AmazonListing>().ToTable("AmazonListing");



            modelBuilder.Entity<KeywordAssignment>()
                .HasKey(c => new { c.KeywordID, c.ProductID, c.Order });
            modelBuilder.Entity<ProductSizes>()
                .HasKey(c => new { c.SizeID, c.ProductID });
            modelBuilder.Entity<AccountMarketplaces>()
                .HasKey(c => new { c.MarketplaceID, c.AccountID });

        }

        public AmazonContext(DbContextOptions<AmazonContext> options) 
            : base(options)
        {
        }

        public AmazonContext()
        {
            
        }

        public DbSet<AmazonBrowseNode> AmazonBrowseNodes { get; set; }

        public DbSet<AmazonAccount> AmazonAccounts { get; set; }

        public DbSet<AmazonColor> AmazonColors { get; set; }

        public DbSet<AmazonCountry> AmazonCountries { get; set; }

        public DbSet<AmazonCurrency> AmazonCurrencies { get; set; }

        public DbSet<AmazonSize> AmazonSizes { get; set; }

        public DbSet<AmazonType> AmazonTypes { get; set; }

        public DbSet<AmazonVariation> AmazonVariations { get; set; }

        public DbSet<AmazonBulletPoint> AmazonBulletPoints { get; set; }

        public DbSet<AmazonImageURL> AmazonImageURLs { get; set; }

        public DbSet<AmazonKeyword> AmazonKeywords { get; set; }

        public DbSet<AmazonCategory> AmazonCategories { get; set; }

        public DbSet<AmazonProduct> AmazonProducts { get; set; }

        public DbSet<AmazonProductColor> AmazonProductColors { get; set; }

        public DbSet<AmazonMarketplace> AmazonMarketplaces { get; set; }

        public DbSet<AmazonDesign> AmazonDesigns { get; set; }

        public DbSet<AmazonItem> AmazonItems { get; set; }

        public DbSet<KeywordAssignment> KeywordAssignment { get; set; }

        public DbSet<AmazonItemSingle> AmazonItemSingles { get; set; }

        public DbSet<ProductSizes> ProductSizes { get; set; }

        public DbSet<AmazonListing> AmazonListings { get; set; }

        public DbSet<AccountMarketplaces> AccountMarketplaces { get; set; }





    }
}