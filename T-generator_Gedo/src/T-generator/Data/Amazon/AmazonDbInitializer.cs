using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Data.Amazon
{
    public class AmazonDbInitializer
    {
        public static void Initialize(AmazonContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
