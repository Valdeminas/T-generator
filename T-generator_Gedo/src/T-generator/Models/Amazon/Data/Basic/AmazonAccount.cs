using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Models.Amazon.Data.Basic
{
    public class AmazonAccount
    {
        public int AmazonAccountID { get; set; }

        [Display(Name = "Account")]
        public string Name { get; set; }
        public string Prefix { get; set; }

        [Display(Name = "Marketplaces")]
        public ICollection<AccountMarketplaces> Marketplaces { get; set; }
    }
}