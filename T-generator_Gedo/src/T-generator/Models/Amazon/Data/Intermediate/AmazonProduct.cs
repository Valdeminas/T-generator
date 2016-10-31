﻿using System.Collections.Generic;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonProduct
    {
        public int AmazonProductID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AmazonKeyword> Keywords { get; set; }
        public virtual AmazonType AmazonType { get; set; }
        public int AmazonTypeID { get; set; }
    }
}