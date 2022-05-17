using BlogManagement_MVC.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace BlogManagement_MVC
{
    public static class SeedData
    {
        public static void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("ADMIN").Result == null)
            {
                var user = new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    RegisteredAt = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(user, "Admin123!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "ADMIN").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Blogger").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Blogger"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Guest").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Guest"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
