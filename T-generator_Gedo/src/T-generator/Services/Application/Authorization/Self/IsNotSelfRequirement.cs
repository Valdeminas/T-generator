using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models;

namespace T_generator.Controllers.Helpers
    {
    public class IsNotSelfRequirement : IAuthorizationRequirement
        {
        public const string ISNOTSELF_POLICY = "IsNotSelf";

        protected bool IsNotSelf { get; set; }

        public IsNotSelfRequirement()
            {
            }
        }
    }
