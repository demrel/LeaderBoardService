using System;
using LeaderBoardService.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace LeaderBoardService.Helper
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
         

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "admin",
                };
               
                IdentityResult result = userManager.CreateAsync(user, "Admin1234").Result;

                if (result.Succeeded)
                {
                  
                    var roleAdmin = new IdentityRole();
                    roleAdmin.Name = "Admin";
                    roleManager.CreateAsync(roleAdmin).Wait();
                    userManager.AddToRoleAsync(user, roleAdmin.Name).Wait();
                }
            }
        }
  
    }
}
