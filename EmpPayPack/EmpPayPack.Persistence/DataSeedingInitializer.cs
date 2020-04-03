using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmpPayPack.Persistence
{
    public static class DataSeedingInitializer
    {
        private const string ADMIN = "Admin";
        private const string MANAGER = "Manager";
        private const string STAFF = "Staff";
        private const string ADMIN_EMAIL = "admin@emppaypack.com";
        private const string ADMIN_PASSWORD = "Adminpassword1";
        private const string MANAGER_EMAIL = "manager@emppaypack.com";
        private const string MANAGER_PASSWORD = "Password2";
        private const string STAFF_EMAIL = "staff@emppaypack.com";
        private const string STAFF_PASSWORD = "Password3";
        private const string NO_ROLE_USER_EMAIL = "johndoe@emppaypack.com";
        private const string NO_ROLE_USER_PASSWORD = "Password10";

        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create User Roles
            string[] roles = new string[3] { ADMIN, MANAGER, STAFF };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if(!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin User
            if(userManager.FindByEmailAsync(ADMIN_EMAIL).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = ADMIN_EMAIL,
                    Email = ADMIN_EMAIL
                };

                IdentityResult identityResult = userManager.CreateAsync(user, ADMIN_PASSWORD).Result;

                if(identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ADMIN).Wait();
                }
            }

            // Create Manager User
            if (userManager.FindByEmailAsync(MANAGER_EMAIL).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = MANAGER_EMAIL,
                    Email = MANAGER_EMAIL
                };

                IdentityResult identityResult = userManager.CreateAsync(user, MANAGER_PASSWORD).Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, MANAGER).Wait();
                }
            }

            // Create Staff User
            if (userManager.FindByEmailAsync(STAFF_EMAIL).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = STAFF_EMAIL,
                    Email = STAFF_EMAIL
                };

                IdentityResult identityResult = userManager.CreateAsync(user, STAFF_PASSWORD).Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, STAFF).Wait();
                }
            }

            // Create No Role User
            if (userManager.FindByEmailAsync(NO_ROLE_USER_EMAIL).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = NO_ROLE_USER_EMAIL,
                    Email = NO_ROLE_USER_EMAIL
                };

                IdentityResult identityResult = userManager.CreateAsync(user, NO_ROLE_USER_PASSWORD).Result;

                // No Role for this user
            }
        }
    }
}
