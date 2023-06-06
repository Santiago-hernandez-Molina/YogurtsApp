using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutryDairyASPApplication.Models;

namespace NutryDairyASPApplication.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<ProductSet> ProductSets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Ingredient_Products> Ingredient_Products { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<ElaborationProcess> ElaborationProcess { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
}
