using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.ViewModels;

namespace NutryDairyASPApplication.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var Products = _context.Products.Take(10).ToList();
        var Blogs = _context.Blogs.Take(10).ToList();
        var Ingredients = _context.Ingredients.Take(10).ToList();
        var ElaborationProcesses = _context.ElaborationProcess.Take(10).ToList();
        AdminVM AdminVM = new AdminVM(){
            Products = Products,
                     Blogs = Blogs,
                     Ingredients= Ingredients,
                     ElaborationProcesses=ElaborationProcesses
        };
        return View(AdminVM);
    }
}
