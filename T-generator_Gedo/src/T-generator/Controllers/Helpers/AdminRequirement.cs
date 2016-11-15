﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Controllers.Helpers
    {
    public class AdminRequirement : IAuthorizationRequirement
        {
        public const string ADMIN_POLICY = "Admin";

        protected bool ShouldBeAdmin { get; set; }

        public AdminRequirement(bool shouldBeAdmin)
            {
            ShouldBeAdmin = shouldBeAdmin;
            }
        }
    }
