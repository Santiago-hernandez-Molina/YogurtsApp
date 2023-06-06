using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Data.Static;
using NutryDairyASPApplication.Models;

namespace NutryDairyASPApplication.Controllers
{
    public class ProductSetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductSetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PartialIndex()
        {
            var data = await _context.ProductSets.ToListAsync();
            return PartialView("_PartialIndex",data);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View(new ProductSet());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create([Bind("Name")]ProductSet data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            _context.ProductSets.Add(data);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index),"Admin");
        }


        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Edit(int? id)
        {

            if (id==null)
            {
                return BadRequest("Bad Request");
            }
            var ProductSet = _context.ProductSets.Find(id);
            if (ProductSet == null)
            {
                return NotFound("Not Found");
            }
            return View(ProductSet);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Edit(int id, [Bind("Id, Name")]ProductSet data)
        {
            if (!ModelState.IsValid  || id != data.Id)
            {
                ModelState.AddModelError("", "¡Hubo un problema, verifica los datos!");
                return View(data);
            }

            _context.ProductSets.Update(data);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index),"Admin");
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(int id)
        {
            var data = _context.ProductSets.FirstOrDefault(a=> a.Id == id);
            if (data == null) { return View("NotFound"); }
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(int id, [Bind("Id, Name")] ProductSet data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if(id == data.Id)
            {
                _context.ProductSets.Remove(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index),"Admin");
            }
            return View("Delete",data);
        }

    }
}
