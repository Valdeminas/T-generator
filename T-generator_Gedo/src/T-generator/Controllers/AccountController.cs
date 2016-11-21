using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using T_generator.Models;
using T_generator.Models.AccountViewModels;
using T_generator.Data;
using T_generator.Controllers.Helpers;

namespace T_generator.Controllers
    {
    public class AccountController : Controller
        {
        private const string PERMISSION_DENIED = "~/Views/Shared/PermissionDenied.cshtml";
        private const string ERROR_OCCURED = "~/Views/Shared/Error.cshtml";
        private const string RESET_PASSWORD_CONFIRMATION = "~/Account/ResetPasswordConfirmation";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory)
            {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            }

        //[HttpDelete]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public IActionResult Delete(string userName, string returnUrl = null)
            {
            ApplicationUser userToDelete;
            try
                {
                userToDelete = CheckRights(userName, UserActions.Delete);
                }
            catch (ArgumentException ex)
                {
                ViewData["Message"] = ex.Message;
                return View(ERROR_OCCURED);
                }

            if (DeleteUser(userToDelete).Result.Succeeded)
                {
                if (returnUrl == null)
                    returnUrl = "~/Account/Users";
                ViewData["ReturnUrl"] = returnUrl;

                _logger.LogInformation(8, "User account was deleted.");
                return RedirectToLocal(returnUrl);
                }

            ViewData["Message"] = "Failed to delete selected user.";
            return View(ERROR_OCCURED);
            }

        //
        // GET: /Account/Modify
        [HttpGet]
        [Authorize(Policy = IsNotSelfRequirement.ISNOTSELF_POLICY)]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]        
        public IActionResult Modify(string userName)
            {
            ApplicationUser userToModify;
            try
                {
                userToModify = CheckRights(userName, UserActions.Modify);
                }
            catch (ArgumentException ex)
                {
                ViewData["Message"] = ex.Message;
                return View(ERROR_OCCURED);
                }

            return View(new ModifyViewModel(userToModify));
            }

        //
        // POST: /Account/Modify
        [HttpPost]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public async Task<IActionResult> Modify(ModifyViewModel model, string returnUrl = null)
            {
            try
                {
                CheckRights(model.Name, UserActions.Modify);
                }
            catch (ArgumentException ex)
                {
                ViewData["Message"] = ex.Message;
                return View(ERROR_OCCURED);
                }
               
            if (ModelState.IsValid)
                {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                user.IsAdmin = model.MakeAdmin;
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    {
                    if (returnUrl == null)
                        returnUrl = "~/Account/Users";
                    ViewData["ReturnUrl"] = returnUrl;

                    _logger.LogInformation(7, "User account was modified.");
                    return RedirectToLocal(returnUrl);
                    }
                }

            return View(model);
            }

        //
        // GET: /Account/Users
        [HttpGet]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public IActionResult Users()
            {
            ViewData["Message"] = "Existing users";
            return View(_userManager.Users.ToList());
            }
    
        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
            {
            ViewData["ReturnUrl"] = returnUrl;
            if (!_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return RedirectToLocal(returnUrl);
            }
            }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
            {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
                {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                    }
                else
                    {
                    return View(model);
                    }
                }

            // If we got this far, something failed, redisplay form
            return View(model);
            }

        //
        // GET: /Account/Register
        [HttpGet]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public IActionResult Register(string returnUrl = null)
            {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
            }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
            {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
                {
                ApplicationUser user = new ApplicationUser { UserName = model.Name, Email = model.Email, IsAdmin = model.MakeAdmin, IsPowerAdmin = false };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    {
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction("Users");
                    }
                AddErrors(result);
                }

            // If we got this far, something failed, redisplay form
            return View(model);
            }

        //
        // POST: /Account/LogOff
        [HttpGet]
        public async Task<IActionResult> LogOff()
            {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
            }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public IActionResult ResetPassword(string userName, string returnUrl = null)
            {
            ApplicationUser userToResetPassword;
            try
                {
                userToResetPassword = CheckRights(userName, UserActions.ResetPassword);
                }
            catch (ArgumentException ex)
                {
                ViewData["Message"] = ex.Message;
                return View(ERROR_OCCURED);
                }

            return View(new ResetPasswordViewModel(userToResetPassword));
            }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [Authorize(Policy = AdminRequirement.ADMIN_POLICY)]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
            {
            try
                {
                CheckRights(model.Name, UserActions.ResetPassword);
                }
            catch (ArgumentException ex)
                {
                ViewData["Message"] = ex.Message;
                return View(ERROR_OCCURED);
                }

            if (!ModelState.IsValid)
                {
                return View(model);
                }
            var user = await _userManager.FindByNameAsync(model.Name);
            if (user == null)
                {
                // Don't reveal that the user does not exist
                return RedirectToAction(RESET_PASSWORD_CONFIRMATION);
                }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
                {
                ViewData["ResetUser"] = model.Name;
                return RedirectToAction(RESET_PASSWORD_CONFIRMATION);
                }
            AddErrors(result);
            return View();
            }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
            {
            foreach (var error in result.Errors)
                {
                ModelState.AddModelError(string.Empty, error.Description);
                }
            }

        private Task<ApplicationUser> GetCurrentUserAsync()
            {
            return _userManager.GetUserAsync(HttpContext.User);
            }

        private IActionResult RedirectToLocal(string returnUrl)
            {
            if (Url.IsLocalUrl(returnUrl))
                {
                return Redirect(returnUrl);
                }
            else
                {
                return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

        private async Task<IdentityResult> DeleteUser(ApplicationUser userToDelete)
            {
            return await _userManager.DeleteAsync(userToDelete);
            }

        private ApplicationUser CheckRights(string changeUserName, UserActions action)
            {
            string outMessage = "";

            ApplicationUser logedUser = GetCurrentUserAsync().Result;
            if (changeUserName == logedUser.UserName)
                {
                switch (action)
                    {
                    case UserActions.Delete:
                        outMessage = "Brace yourself - winter is coming. And do not try to delete yourself.";
                        break;
                    case UserActions.Modify:
                        outMessage = "Brace yourself - winter is coming. And do not try to modify yourself.";
                        break;
                    case UserActions.ResetPassword:
                        outMessage = "Brace yourself - winter is coming. And do not try to reset password for yourself.";
                        break;
                    }

                throw new ArgumentException(outMessage);
                }
                
            ApplicationUser userToChange = _userManager.Users.First(x => x.UserName == changeUserName);
            if (null == userToChange)
                {
                switch (action)
                    {
                    case UserActions.Delete:
                        outMessage = "No user selected for deletion.";
                        break;
                    case UserActions.Modify:
                        outMessage = "No user selected for modification";
                        break;
                    case UserActions.ResetPassword:
                        outMessage = "No user selected for password reset.";
                        break;
                    }

                throw new ArgumentException(outMessage);
                }
                
            if (userToChange.IsPowerAdmin)
                {
                switch (action)
                    {
                    case UserActions.Delete:
                        outMessage = "Impossible to delete this user.";
                        break;
                    case UserActions.Modify:
                        outMessage = "Impossible to modify this user.";
                        break;
                    case UserActions.ResetPassword:
                        outMessage = "Impossible to reset password for this user.";
                        break;
                    }

                throw new ArgumentException(outMessage);
                }
                
            if (!(logedUser.IsPowerAdmin ||
                 (logedUser.IsAdmin && !userToChange.IsAdmin)))
                {
                switch (action)
                    {
                    case UserActions.Delete:
                        outMessage = "You do not have enough rights to delete this user.";
                        break;
                    case UserActions.Modify:
                    case UserActions.ResetPassword:
                        outMessage = "You do not have enough rights to modify this user.";
                        break;
                    }

                throw new ArgumentException(outMessage);
                }

            return userToChange;
            }
        #endregion
        }

    enum UserActions
        {
        Delete = 0,
        Modify = 1,
        ResetPassword = 2,
        }
    }
