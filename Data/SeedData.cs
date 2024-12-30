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
                    new ThreadCategory { Name = "General Discussion", Description = "Random crap goes here" }, // ID = 1
                    new ThreadCategory { Name = "Cocks", Description = "Pictures of cocks go here" }, // 2
                    new ThreadCategory { Name = "Genshin Discussion", Description = "GenSHIT SHITpact"} // 3
                };
                context.AddRange(categories);

                // thread groups
                var groups = new List<ThreadGroup>
                {
                    new ThreadGroup { Name = "religion", Description = "Bismallah", CategoryId = 1 },
                    new ThreadGroup { Name = "fitness", Description = "huge men", CategoryId = 1 },

                    new ThreadGroup { Name = "small cocks", Description = "yum", CategoryId = 2 },
                    new ThreadGroup { Name = "big cocks", Description = "yum yum", CategoryId = 2 },

                    new ThreadGroup { Name = "GODpitano", Description = "I FUARKING kneel", CategoryId = 3 },
                    new ThreadGroup { Name = "FVARKA", Description = "sex with capitano", CategoryId = 3 },
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

                var newAdminUser = new User()
                {
                    UserName = "chadmin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser);
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                var newAppUser = new User()
                {
                    UserName = "gwuser",
                    Email = "gwedin@gmail.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAppUser);
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}
