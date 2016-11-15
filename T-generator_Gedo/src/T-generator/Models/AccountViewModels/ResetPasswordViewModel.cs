using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public ResetPasswordViewModel(ApplicationUser userToResetPassword)
            {
            Name = userToResetPassword.UserName;
            }

        public ResetPasswordViewModel()
            {
            }
    }
}
