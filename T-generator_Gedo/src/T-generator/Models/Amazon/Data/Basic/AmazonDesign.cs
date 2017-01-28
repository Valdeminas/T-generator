using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Data.Basic
{
    public class AmazonDesign
    {
        public int AmazonDesignID { get; set; }

        public string Name { get; set; }
        //public string Prefix { get; set; }

        public string DesignURL { get; set; }

        public string SearchTags1 { get; set; }
        public string SearchTags2 { get; set; }
        public string SearchTags3 { get; set; }

        [Display(Name = "Account")]
        public int AmazonAccountID { get; set; }

        public virtual AmazonAccount AmazonAccount { get; set; }

        [Display(Name = "Category")]
        public int AmazonCategoryID { get; set; }

        public virtual AmazonCategory AmazonCategory { get; set; }
    }
}