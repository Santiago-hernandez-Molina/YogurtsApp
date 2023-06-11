using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.Data.Static;
using X.PagedList;

namespace NutryDairyASPApplication.Controllers;

public class ArticleController : Controller
{

    private readonly ApplicationDbContext _context;

    public ArticleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? id, string SortOn, string orderBy, string pSortOn, int? page)
    {
        int BlogId = id??_context.Blogs.FirstOrDefault().Id;
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
        var data = _context.Articles.Where(a => a.BlogId==BlogId)
            .AsQueryable();
        switch (SortOn)
        {
            case "Title":
                data = orderBy.Equals("asc") ? data.OrderBy(i => i.Title): data.OrderByDescending(i=>i.Title);
                break;
            default:
                data = data.OrderBy(i=>i.Id);
                break;
        }
        ViewBag.Articles = data.ToPagedList(page.Value, recordsPerPage);
        return View();
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Create(int id)
    {
        if (id == 0)
        {
            return View("AdminIndex","Blog");
        }
        var data = new Article();
        data.BlogId = id;
        return View(data);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Create([Bind("Title, RelatedImagePath, BlogId, Paragraphs")]Article data)
    {
        if (!ModelState.IsValid)
        {
            return View(data);
        }

            _context.Articles.Add(data);
            _context.SaveChanges();
            return RedirectToAction("AdminIndex","Blog");
    }

    public IActionResult Detail(int? id)
    {
        if (id==null)
        {
            return BadRequest("Bad Request");
        }
        var Article = _context.Articles.FirstOrDefault(a=>a.Id == id);
        if (Article == null)
        {
            return NotFound("Not Found");
        }
        ViewBag.Title = Article.Title;
        return View(Article);
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Edit(int? id)
    {

        if (id==null)
        {
            return BadRequest("Bad Request");
        }
        var Article = _context.Articles.Find(id);
        if (Article == null)
        {
            return NotFound("Not Found");
        }
        return View(Article);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Edit(int id, [Bind("Id, Title, RelatedImagePath, BlogId, Paragraphs")]Article data)
    {
        if (!ModelState.IsValid  || id != data.Id)
        {
            ModelState.AddModelError("", "¡Hubo un problema, verifica los datos y la Imagen!");
            return View(data);
        }

        _context.Articles.Update(data);
        _context.SaveChanges();
        return RedirectToAction("AdminIndex","Blog");
    }

    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Delete(int id)
    {
        var data = _context.Articles.FirstOrDefault(a=> a.Id == id);
        if (data == null) { return View("NotFound"); }
        return View(data);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public IActionResult Delete(int id, [Bind("Id, Title, RelatedImagePath, BlogId, Paragraphs")]Article data)
    {
        if (!ModelState.IsValid)
        {
            return View(data);
        }
        if(id == data.Id)
        {
            _context.Articles.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("AdminIndex","Blog");
        }
        return View("Delete",data);
    }

}
