using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T_generator.Models;
using Microsoft.AspNetCore.Identity;

namespace T_generator.Data
    {
    public class DefaultUsersSeedData
        {
        // List users in such way: userEmail, userName, userPassword, makeAdmin, makePowerAdmin
        private static readonly List<Tuple<string, string, string, bool, bool>> DefaultUsersData = new List<Tuple<string, string, string, bool, bool>>
            {
            new Tuple<string, string, string, bool, bool>("admin@admin.com", "admin", "Q!w2e3r4", true, true)
            };

        private static List<Tuple<ApplicationUser, string>> DefaultUsers
            {
            get
                {
                List<Tuple<ApplicationUser, string>> result = new List<Tuple<ApplicationUser, string>>();
                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

                foreach (Tuple<string, string, string, bool, bool> currentDefaultUser in DefaultUsersData)
                    {
                    ApplicationUser user = new ApplicationUser {
                        UserName = currentDefaultUser.Item2,
                        Email = currentDefaultUser.Item1,
                        IsAdmin = currentDefaultUser.Item4,
                        IsPowerAdmin = currentDefaultUser.Item5
                        };

                    result.Add(new Tuple<ApplicationUser, string>(user, currentDefaultUser.Item3));
                    }

                return result;
                }
            }

        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public DefaultUsersSeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            {
            _context = context;
            _userManager = userManager;
            }

        public async Task AddDefaultUsers()
            {
            foreach (Tuple<ApplicationUser, string> currentUser in DefaultUsers)
                {
                ApplicationUser userToCreate = currentUser.Item1;
                if (_context.Users.Where(x => x.Email == userToCreate.Email).Count() == 0)
                    {
                    IdentityResult result = await _userManager.CreateAsync(userToCreate, currentUser.Item2);
                    if (!result.Succeeded)
                        throw new Exception("Server set up failed");
                    }
                }

            await _context.SaveChangesAsync();
            }

        public static bool IsUserDefault(ApplicationUser user)
            {
            foreach (Tuple<string, string, string, bool, bool> thisUser in DefaultUsersData)
                if (thisUser.Item1 == user.Email)
                    return true;

            return false;
            }

        public static bool IsUserDefault(string userEmail)
            {
            foreach (Tuple<string, string, string, bool, bool> thisUser in DefaultUsersData)
                if (thisUser.Item1 == userEmail)
                    return true;

            return false;
            }
        }
    }
