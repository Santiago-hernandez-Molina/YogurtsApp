using Microsoft.AspNetCore.Identity;
using NutryDairyASPApplication.Models;
using ustaTickets.Data.Static;

namespace NutryDairyASPApplication.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                // Roles
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (await roleManager.FindByNameAsync(UserRoles.User) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                // Users
                string email = "andres.diazs@usantoto.edu.co";
                string password = "Ust4.T1ck3ts";
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                if (await userManager.FindByNameAsync(email) == null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = email,
                                 Email = email,
                                 PhoneNumber = "6087440404",
                                 EmailConfirmed = true,
                                 CityId = 1,

                    };
                    var result = await userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await userManager.AddPasswordAsync(user, password);
                        await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }
                }
                
            }
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>(); 
                context.Database.EnsureCreated();

                if (!context.Products.Any())
                {
                    context.ProductSets.AddRange( new List<ProductSet>()
                            {
                            new ProductSet
                            {
                                Name = "Yogurt"
                            }
                            });
                }

                context.SaveChanges();

                if (!context.Products.Any())
                {
                    context.Products.AddRange( new List<Product>()
                            {
                            new Product
                            {
                                Name = "Yogurt Griego",
                                Description = "Lore impsum",
                                Calories = (decimal)10.09,
                                Fats = (decimal) 70.0,
                                Proteins = (decimal) 30.0,
                                Price = (decimal) 15.000,
                                Stock = 10,
                                ImagePath = " ",
                                ProductSetId = 1,
                            },
                            new Product
                            {
                                Name = "Yogurt de Fresa",
                                Description = "Lore impsum",
                                Calories = (decimal)10.09,
                                Fats = (decimal) 70.0,
                                Proteins = (decimal) 30.0,
                                Price = (decimal) 15.000,
                                Stock = 10,
                                ImagePath = " ",
                                ProductSetId = 1,
                            },
                            new Product
                            {
                                Name = "Yogurt De Mora",
                                Description = "Lore impsum",
                                Calories = (decimal)10.09,
                                Fats = (decimal) 70.0,
                                Proteins = (decimal) 30.0,
                                Price = (decimal) 15.000,
                                Stock = 10,
                                ImagePath = " ",
                                ProductSetId = 1,
                            },
                            });
                context.SaveChanges();

                }

                if (!context.Blogs.Any())
                {
                    context.Blogs.AddRange(new List<Blog>()
                            {
                            new Blog
                            {
                                Description = "\"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.\"",
                            },
                            new Blog
                            {
                                Description = "\"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.\"",
                            },
                            new Blog
                            {
                                Description = "\"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.\"",
                            },
                            });
                    context.SaveChanges();

                }
            }
        }
    }
}
