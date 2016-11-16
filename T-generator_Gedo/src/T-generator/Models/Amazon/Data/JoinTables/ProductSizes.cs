using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Data.JoinTables
{
    public class ProductSizes
    {
        public int SizeID { get; set; }
        public int ProductID { get; set; }

        public AmazonSize Size { get; set; }
        public AmazonProduct Product { get; set; }
    }
}
