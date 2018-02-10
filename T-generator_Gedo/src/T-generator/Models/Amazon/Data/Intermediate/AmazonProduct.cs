using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonProduct
    {
        public int AmazonProductID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Prefix { get; set; }

        public string Description { get; set; }        

        [Display(Name = "Type")]
        public int AmazonTypeID { get; set; }
        public virtual AmazonType AmazonType { get; set; }

        public ICollection<KeywordAssignment> Keywords { get; set; }

        [Display(Name = "Possible Sizes")]
        public ICollection<ProductSizes> Sizes { get; set; }

        [Display(Name = "Account")]
        public int AmazonAccountID { get; set; }
        public virtual AmazonAccount AmazonAccount { get; set; }

    }
}