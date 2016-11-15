using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Models.AccountViewModels
    {
    public class ModifyViewModel
        {
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Name { get; set; }

        public bool MakeAdmin { get; set; }

        public ModifyViewModel(ApplicationUser userToModify)
            {
            Email = userToModify.Email;
            Name = userToModify.UserName;
            MakeAdmin = userToModify.IsAdmin;
            }

        public ModifyViewModel()
            {
            }
        }
    }
