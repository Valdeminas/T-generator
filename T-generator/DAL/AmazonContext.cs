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

        public DbSet<AmazonAccount> AmazonAccounts { get; set; }
        public DbSet<AmazonProduct> AmazonProducts { get; set; }
        public DbSet<AmazonColorVariation> AmazonColorVariations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}