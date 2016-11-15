using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models;

namespace T_generator.Controllers.Helpers
    {
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
        {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminHandler(UserManager<ApplicationUser> userManager)
            {
            _userManager = userManager;
            }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
            {
            if (_userManager.GetUserAsync(context.User).Result.IsAdmin)
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
            }
        }
    }
