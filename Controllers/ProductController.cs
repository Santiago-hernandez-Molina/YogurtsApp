using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.Utils;
using NutryDairyASPApplication.Data.Static;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace NutryDairyASPApplication.Controllers;

public class ProductController : Controller
{

  private readonly ApplicationDbContext _context;

  public ProductController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET
  public IActionResult Index()
  {
    var data = _context.Products.ToList();
    return View(data);
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
    var data = _context.Products.AsQueryable();
    switch (SortOn)
    {
      case "Name":
        data = orderBy.Equals("asc") ? data.OrderBy(i => i.Name): data.OrderByDescending(i=>i.Name);
        break;
      case "Price":
        data = orderBy.Equals("asc") ? data.OrderBy(i => i.Price): data.OrderByDescending(i=>i.Price);
        break;
      default:
        data = data.OrderBy(i=>i.Id);
        break;
    }
    ViewBag.products = data.ToPagedList(page.Value, recordsPerPage);
    return View();
  }

  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Create()
  {
    ViewBag.Categories = _context.ProductSets.ToList();
    return View(new Product());
  }

  [HttpPost]
  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Create([Bind("Id, Name, Quantity, Archivo, Description, Price, Proteins, Calories, Fats, Stock, ProductSetId")]Product data)
  {
    if (!ModelState.IsValid)
    {
      return View(data);
    }

    if (data.Archivo != null && data.Archivo.Length > 0 )
    {
      var saveFiles = new SaveFiles();
      data.ImagePath = saveFiles.SaveImageToBase64(data.Archivo);
    }else
    {
      data.ImagePath = "";
    }
    try
    {
      _context.Products.Add(data);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index),"Admin");
    }
    catch(DbUpdateException)
    {
      ModelState.AddModelError("", "La categoria no existe!");
      return View(data);
    }
  }

  public IActionResult Detail(int? id)
  {
    if (id==null)
    {
      return BadRequest("Bad Request");
    }
    var Product = _context.Products.Find(id);
    if (Product == null)
    {
      return NotFound("Not Found");
    }
    return View(Product);
  }

  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Edit(int? id)
  {

    if (id==null)
    {
      return BadRequest("Bad Request");
    }
    var Product = _context.Products.Find(id);
    if (Product == null)
    {
      return NotFound("Not Found");
    }
    ViewBag.Categories = _context.ProductSets.ToList();
    return View(Product);
  }

  [HttpPost]
  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Edit(int id, [Bind("Id, Name, Quantity, ImagePath, Archivo, Description, Price, Proteins, Calories, Fats, Stock, ProductSetId")]Product data)
  {
    if (!ModelState.IsValid  || id != data.Id)
    {
      ModelState.AddModelError("", "¡Hubo un problema, verifica los datos y la Imagen!");
      return View(data);
    }

    if (data.Archivo != null && data.Archivo.Length > 0 )
    {
      if (data.Archivo != null && data.Archivo.Length > 0 )
      {
        var saveFiles = new SaveFiles();
        data.ImagePath = saveFiles.SaveImageToBase64(data.Archivo);
      }
    }
    try{
      _context.Products.Update(data);
      _context.SaveChanges();
    }
    catch(DbUpdateException)
    {
      ModelState.AddModelError("", "La categoria no existe!");
      return View(data);
    }
    return RedirectToAction(nameof(Index),"Admin");
  }

  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Delete(int id)
  {
    var data = _context.Products.FirstOrDefault(a=> a.Id == id);
    if (data == null) { return View("NotFound"); }
    return View(data);
  }

  [HttpPost]
  [Authorize(Roles = UserRoles.Admin)]
  public IActionResult Delete(int id, [Bind("Id, Name, ImagePath, Description, Price, Proteins, Calories, Fats, Stock, ProductSetId")] Product Product)
  {
    if (!ModelState.IsValid)
    {
      return View(Product);
    }
    if(id == Product.Id)
    {
      _context.Products.Remove(Product);
      _context.SaveChanges();
      //return RedirectToAction(nameof(Index),"Admin");
      return RedirectToAction(nameof(AdminIndex));
    }
    return View("Delete",Product);
  }

  public Product GetProductById(int id)
  {
    return _context.Products
      .FirstOrDefault(p => p.Id == id);
  }
}
