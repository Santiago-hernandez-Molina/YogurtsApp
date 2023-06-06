using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Data.Static;
using NutryDairyASPApplication.ViewModels;

namespace NutryDairyASPApplication.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Index()
    {
        AdminVM AdminVM = new AdminVM{
            TotalProducts = _context.Products.Count(),
            TotalBlogs = _context.Blogs.Count(),
        };
        return View(AdminVM);
    }
}
