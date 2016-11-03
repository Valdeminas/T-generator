using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Data.JoinTables
{
    public class AmazonKeywordAssignment
    {
        public int AmazonKeywordID { get; set; }
        public int AmazonProductID { get; set; }
        public AmazonKeyword AmazonKeyword { get; set; }
        public AmazonProduct AmazonProduct { get; set; }
    }
}
