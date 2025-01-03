using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Check if data already exists
                if (context.ThreadCategories.Any() || context.Threads.Any() || context.ThreadReplies.Any() || context.ThreadGroups.Any())
                {
                    return; // Database already seeded
                }

                // thread categories
                var categories = new List<ThreadCategory>
                {
                    new ThreadCategory { Name = "Category 1", Description = "Cat 1 desc" }, 
                    new ThreadCategory { Name = "Category 2", Description = "Cat 2 desc" }, 
                    new ThreadCategory { Name = "Category 3", Description = "Cat 3 desc"} 
                };
                context.AddRange(categories);

                // thread groups
                var groups = new List<ThreadGroup>
                {
                    new ThreadGroup { Name = "Group 1", Description = "Group 1 desc", CategoryId = 1 },
                    new ThreadGroup { Name = "Group 2", Description = "Group 2 desc", CategoryId = 1 },

                    new ThreadGroup { Name = "Group 3", Description = "Group 3 desc", CategoryId = 2 },
                    new ThreadGroup { Name = "Group 4", Description = "Group 4 desc", CategoryId = 2 },

                    new ThreadGroup { Name = "Group 5", Description = "Group 5 desc", CategoryId = 3 },
                    new ThreadGroup { Name = "Group 6", Description = "Group 6 desc", CategoryId = 3 },
                };
                context.AddRange(groups);

                context.SaveChanges();
            }
        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                string appUserEmail = "admin@admin.com";
                var existingAppUser = userManager.FindByEmailAsync(appUserEmail);
                if (existingAppUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "user",
                        Email = "user@user.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string adminEmail = "admin@admin.com";
                var existingAdmin = userManager.FindByEmailAsync(adminEmail);
                if (existingAdmin == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
