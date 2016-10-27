using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Data.Dump
{
    public class AmazonKeyword
    {
        public int AmazonKeywordID { get; set; }
        public string Keyword { get; set; }

        public virtual ICollection<AmazonProduct> AmazonProduct { get; set; }
    }
}