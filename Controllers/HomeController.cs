using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.ViewModels;

namespace NutryDairyASPApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var data = _context.Products.Take(3).OrderBy(p => p.Name).ToList();
        var blogs = _context.Blogs
            .Include(b => b.Articles.Take(3))
            .ToList();
        HomeVM homevm = new HomeVM
        {
            products = data,
            blogs = blogs
        };
        return View(homevm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
