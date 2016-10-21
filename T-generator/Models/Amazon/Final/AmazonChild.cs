using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonChild
    {
        public int AmazonChildID { get; set; }

        public string ImageURL { get; set; }

        public int AmazonParentTemplateID {get; set;} 
    }
}