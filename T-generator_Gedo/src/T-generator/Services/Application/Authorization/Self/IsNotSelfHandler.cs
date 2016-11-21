using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models;

namespace T_generator.Controllers.Helpers
{
    public class IsNotSelfHandler : AuthorizationHandler<IsNotSelfRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IsNotSelfHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsNotSelfRequirement requirement)
        {
            var mvcContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            if (mvcContext != null)
            {
                if (mvcContext.HttpContext.User.Identity.Name != mvcContext.HttpContext.Request.Query["userName"])
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
