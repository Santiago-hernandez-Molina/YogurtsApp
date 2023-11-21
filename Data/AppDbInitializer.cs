using Microsoft.AspNetCore.Identity;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.Data.Static;

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
                                ImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Turkish_strained_yogurt.jpg/220px-Turkish_strained_yogurt.jpg",
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
                                ImagePath = "data:image/webp;base64,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",
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
                                ImagePath = "data:image/jpeg;base64,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",
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
                                    Name = "Recetas",
                                    ImagePath = "/assets/img/Recetas-Img.jpg",
                                    Description = "\"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.\"",
                                },
                                new Blog
                                {
                                    Name = "Acerca de Nuestros Productos",
                                    ImagePath = "/assets/img/Productos-Blog.jpg",
                                    Description = "\"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.\"",
                                },
                            });
                    context.SaveChanges();
                }
                if(!context.Departments.Any())
                {
                    context.Departments.AddRange(new List<Department>()
                            {
                            new Department
                            {
                                Name = "Boyacá",
                                DaneCode = "15",
                            }
                            });
                    context.SaveChanges();
                }
                if(!context.Cities.Any())
                {
                    context.Cities.AddRange(new List<City>()
                            {
                            new City
                            {
                                Name = "Tunja",
                                DaneCode = "15.001",
                                Region = "Centro Oriente",
                                DepartmentId = 1
                            }
                            });
                    context.SaveChanges();
                }
                if(!context.Articles.Any())
                {
                    context.Articles.AddRange(new List<Article>()
                            {
                            new Article
                            {
                                Title = "Cereal",
                                RelatedImagePath = "https://st.depositphotos.com/1007298/3280/i/450/depositphotos_32807141-stock-photo-various-kids-cereals-in-colorful.jpg",
                                BlogId = context.Blogs.FirstOrDefault().Id,
                                Paragraphs = "Lorem ipsum dolor sit amet, qui minim labore adipisicing minim sint cillum sint consectetur cupidatat."

                            },
                            new Article
                            {
                                Title = "Gandores de concurso",
                                RelatedImagePath = "https://cdn-icons-png.flaticon.com/512/4661/4661633.png",
                                BlogId = context.Blogs.Where((b)=>b.Name == "Acerca de Nuestros Productos").FirstOrDefault().Id,
                                Paragraphs = "Lorem ipsum dolor sit amet, qui minim labore adipisicing minim sint cillum sint consectetur cupidatat."
                            }
                            });
                    context.SaveChanges();
                }

            }
        }
    }
}
