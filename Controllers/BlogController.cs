using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.Data.Static;
using FluentFTP;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace NutryDairyASPApplication.Controllers;

public class BlogController : Controller
{

    private readonly ApplicationDbContext _context;

    public BlogController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string SortOn, string orderBy, string pSortOn, int? page)
    {
        int recordsPerPage = 2;
        if(!page.HasValue)
        {
            page = 1;
            orderBy = string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc") ? "desc" : "asc";
        }
        if (!string.IsNullOrWhiteSpace(SortOn) && !SortOn.Equals(pSortOn, StringComparison.CurrentCultureIgnoreCase))
        {
            orderBy = "asc";
        }

        ViewBag.OrderBy = orderBy;
        ViewBag.SortOn = SortOn;
        var data = _context.Blogs.AsQueryable();
        switch (SortOn)
        {
            case "Name":
                data = orderBy.Equals("asc") ? data.OrderBy(i => i.Name): data.OrderByDescending(i=>i.Name);
                break;
            default:
                data = data.OrderBy(i=>i.Id);
                break;
        }
        ViewBag.blogs = data.ToPagedList(page.Value, recordsPerPage);
        return View();
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult AdminIndex(string SortOn, string orderBy, string pSortOn, int? page)
    {
        int recordsPerPage = 5;
        if(!page.HasValue)
        {
            page = 1;
            orderBy = string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc") ? "desc" : "asc";
        }
        if (!string.IsNullOrWhiteSpace(SortOn) && !SortOn.Equals(pSortOn, StringComparison.CurrentCultureIgnoreCase))
        {
            orderBy = "asc";
        }

        ViewBag.OrderBy = orderBy;
        ViewBag.SortOn = SortOn;
        var data = _context.Blogs
            .Include(b=>b.Articles)
            .AsQueryable();
        switch (SortOn)
        {
            case "Name":
                data = orderBy.Equals("asc") ? data.OrderBy(i => i.Name): data.OrderByDescending(i=>i.Name);
                break;
            default:
                data = data.OrderBy(i=>i.Id);
                break;
        }
        ViewBag.blogs = data.ToPagedList(page.Value, recordsPerPage);
        return View();
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Create()
    {
        return View(new Blog());
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Create([Bind("Id, Name, Description, ImagePath")]Blog data)
    {
        if (!ModelState.IsValid)
        {
            return View(data);
        }
        _context.Blogs.Add(data);
        _context.SaveChanges();
        return RedirectToAction(nameof(AdminIndex));
    }

    public IActionResult Detail(int? id)
    {
        if (id==null)
        {
            return BadRequest("Bad Request");
        }
        var Blog = _context.Blogs.Find(id);
        if (Blog == null)
        {
            return NotFound("Not Found");
        }
        return View(Blog);
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Edit(int? id)
    {

        if (id==null)
        {
            return BadRequest("Bad Request");
        }
        var Blog = _context.Blogs.Find(id);
        if (Blog == null)
        {
            return NotFound("Not Found");
        }
        return View(Blog);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Edit(int id, [Bind("Id, Name,Description, ImagePath")]Blog data)
    {
        if (!ModelState.IsValid  || id != data.Id)
        {
            ModelState.AddModelError("", "¡Hubo un problema, verifica los datos y la Imagen!");
            return View(data);
        }
        _context.Blogs.Update(data);
        _context.SaveChanges();
        return RedirectToAction(nameof(AdminIndex));
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Delete(int id)
    {
        var data = _context.Blogs.FirstOrDefault(a=> a.Id == id);
        if (data == null) { return View("NotFound"); }
        return View(data);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Delete(int id, [Bind("Id, Name, Description")] Blog Blog)
    {
        if (!ModelState.IsValid)
        {
            return View(Blog);
        }
        if(id == Blog.Id)
        {
            _context.Blogs.Remove(Blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(AdminIndex));
        }
        return View("Delete",Blog);
    }

}
