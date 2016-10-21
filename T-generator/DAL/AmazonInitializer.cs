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
        }
    }
}