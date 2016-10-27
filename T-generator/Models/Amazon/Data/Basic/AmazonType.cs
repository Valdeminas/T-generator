using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Basic
{
    public class AmazonType
    {
        public int AmazonTypeID { get; set; }

        [Display(Name="Type")]
        public string Name { get; set; }

        public string Prefix { get; set; }
    }
}