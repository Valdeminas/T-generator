using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_generator.Models.Amazon.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Templates.Items;
using T_generator.Resources.DataStructures;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonItemSingle : AmazonItem
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}