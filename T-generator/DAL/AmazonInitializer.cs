using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using T_generator.Models;

namespace T_generator.DAL
{
    public class AmazonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AmazonContext>
    {
        protected override void Seed(AmazonContext context)
        {
            var amazonAccounts = new List<AmazonAccount>
            {
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"},
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"},
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"},
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"},
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"},
            new AmazonAccount{Name="NO KIDDING",BrandName="NO KIDDING",SKUPrefix="NOKID"}
            };

            amazonAccounts.ForEach(s => context.AmazonAccounts.Add(s));
            context.SaveChanges();

            var amazonProducts = new List<AmazonProduct>
            {
            new AmazonProduct{AmazonProductID=1,Title="asd",Description="asd",MetaDescription="asd",Type="asd",TypeShort="asd",BrowseNode="asd",DepartmentName="asd",StaticSearchtags="asd",BulletPoints="asd",Template=Template.Clothing,Size=Size.Large}
            };
            amazonProducts.ForEach(s => context.AmazonProducts.Add(s));
            context.SaveChanges();

            var amazonColorVariations = new List<AmazonColorVariation>
            {
            new AmazonColorVariation{Name="asd",NameShort="asd",Price=12.00,Image="asd",AmazonProductID=1},
            new AmazonColorVariation{Name="asd",NameShort="asd",Price=12.00,Image="asd",AmazonProductID=1},
            new AmazonColorVariation{Name="asd",NameShort="asd",Price=12.00,Image="asd",AmazonProductID=1}
            };
            amazonColorVariations.ForEach(s => context.AmazonColorVariations.Add(s));
            context.SaveChanges();

        }
    }
}