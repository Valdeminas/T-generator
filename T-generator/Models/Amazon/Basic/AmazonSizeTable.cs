using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Basic
{
    public class AmazonSizeTable
    {
        public int AmazonSizeTableID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        public string ImageURL { get; set; }
    }
}