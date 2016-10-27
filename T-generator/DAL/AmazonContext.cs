using T_generator.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace T_generator.DAL
{
    public class AmazonContext : DbContext
    {
        public AmazonContext() : base("AmazonContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<T_generator.Models.AmazonBrowseNode> AmazonBrowseNodes { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.AmazonAccount> AmazonAccounts { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.AmazonColor> AmazonColors { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Basic.AmazonCountry> AmazonCountries { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Basic.AmazonCurrency> AmazonCurrencies { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.AmazonSize> AmazonSizes { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Basic.AmazonType> AmazonTypes { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Basic.AmazonVariation> AmazonVariations { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Data.Dump.AmazonBulletPoint> AmazonBulletPoints { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Data.Dump.AmazonImageURL> AmazonImageURLs { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.Amazon.Data.Dump.AmazonKeyword> AmazonKeywords { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.AmazonProduct> AmazonProducts { get; set; }

        public System.Data.Entity.DbSet<T_generator.Models.AmazonMarketplace> AmazonMarketplaces { get; set; }

    }
}