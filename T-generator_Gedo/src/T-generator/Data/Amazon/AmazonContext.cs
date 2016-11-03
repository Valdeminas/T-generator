using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;
using Microsoft.EntityFrameworkCore;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Data
{
    public class AmazonContext : DbContext
    {
        public AmazonContext(DbContextOptions<AmazonContext> options) : base(options)
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

        public DbSet<AmazonProduct> AmazonProducts { get; set; }

        public DbSet<AmazonKeywordAssignment> AmazonKeywordAssignments { get; set; }

        public DbSet<AmazonMarketplace> AmazonMarketplaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            modelBuilder.Entity<AmazonProduct>().ToTable("AmazonProduct");
            modelBuilder.Entity<AmazonMarketplace>().ToTable("AmazonMarketplace");
            modelBuilder.Entity<AmazonKeywordAssignment>().ToTable("AmazonKeywordAssignment");

            modelBuilder.Entity<AmazonKeywordAssignment>()
                .HasKey(c => new { c.AmazonKeywordID, c.AmazonProductID });
        }

    }
}