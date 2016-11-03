using System.Collections.Generic;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Models.Amazon.Data.Dump
{
    public class AmazonKeyword
    {
        public int AmazonKeywordID { get; set; }
        public string Keyword { get; set; }

        public virtual ICollection<AmazonKeywordAssignment> AmazonProducts { get; set; }
    }
}