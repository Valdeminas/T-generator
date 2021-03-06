﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T_generator.Models.Amazon.Data.Basic
{
    public class AmazonType
    {
        public int AmazonTypeID { get; set; }

        [Display(Name="Type")]
        public string Name { get; set; }

        public string Prefix { get; set; }
    }
}