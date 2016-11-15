using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Data.JoinTables
{
    public class KeywordAssignment
    {
        public int KeywordID { get; set; }
        public int ProductID { get; set; }

        public AmazonKeyword Keyword { get; set; }
        public AmazonProduct Product { get; set; }

        public int Order { get; set; }
    }
}
