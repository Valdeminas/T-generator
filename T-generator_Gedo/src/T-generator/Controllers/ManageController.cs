﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using T_generator.Models;
using T_generator.Models.ManageViewModels;

namespace T_generator.Controllers
    {
    public class ManageController : Controller
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public ManageController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILoggerFactory loggerFactory)
            {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ManageController>();
            }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword(ManageMessageId? message = null)
            {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";
            return View();
            }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
            {           
            if (!ModelState.IsValid)
                {
                return View(model);
                }
            var user = await GetCurrentUserAsync();
            if (user != null)
                {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                    {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(ChangePassword), new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                AddErrors(result);
                return View(model);
                }
            return RedirectToAction(nameof(ChangePassword), new { Message = ManageMessageId.Error });
            }

        #region Helpers
        private void AddErrors(IdentityResult result)
            {
            foreach (var error in result.Errors)
                {
                ModelState.AddModelError(string.Empty, error.Description);
                }
            }

        public enum ManageMessageId
            {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
            }

        private Task<ApplicationUser> GetCurrentUserAsync()
            {
            return _userManager.GetUserAsync(HttpContext.User);
            }
        #endregion
        }
    }
