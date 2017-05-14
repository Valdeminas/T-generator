using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonMarketplace
    {
        public int AmazonMarketplaceID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        [Display(Name = "Currency")]
        public int AmazonCurrencyID { get; set; }

        public virtual AmazonCurrency AmazonCurrency { get; set; }

        [Display(Name = "Template")]
        public string TemplateURL { get; set; }

        [Display(Name = "Sheet Number")]
        public int SheetNumber { get; set; }

        [Display(Name = "Starting Row")]
        public int StartingRow { get; set; }
    }
}