using System.Net;
using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Models;

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
        return View();
    }

    public IActionResult Create()
    {
        return View(new Product());
    }

    [HttpPost]
    public IActionResult Create([Bind("Id, Name, Archivo, Description, Price, Proteins, Calories, Fats, Stock, ProductSetId")]Product Product)
    {
        string ruta = "://localhost/nutredairy/";
        if (Product.Archivo != null && Product.Archivo.Length > 0)
        {
            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Product.Archivo.FileName);
            var rutaDestino = "ftp"+ ruta + nombreArchivo;
            var solicitud = (System.Net.FtpWebRequest)WebRequest.Create(rutaDestino);
            solicitud.Credentials = new NetworkCredential("nutredairy", "123456");
            solicitud.Method = WebRequestMethods.Ftp.UploadFile;
            using (var stream = Product.Archivo.OpenReadStream())
                using (var reader = new BinaryReader(stream))
                {
                    var contenido = reader.ReadBytes((int)Product.Archivo.Length);
                    using (var ftpStream = solicitud.GetRequestStream())
                    {
                        ftpStream.Write(contenido, 0, contenido.Length);
                    }
                }
            // Actualizar el modelo con la URL de la imagen en el servidor FTP
            var rutaAcceso = "http"+ ruta + nombreArchivo;
            Product.ImagePath = rutaAcceso;
        }

        if (!ModelState.IsValid)
        {
            return View(Product);
        }
        _context.Products.Add(Product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index),"Admin");
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
        return View(Product);
    }

    [HttpPost]
    public IActionResult Edit(int id, [Bind("Id, Name, Archivo, Description, Price, Proteins, Calories, Fats, Stock, ProductSetId")]Product Product)
    {
        if (!ModelState.IsValid)
        {
            return View(Product);
        }
        string ruta = "://localhost/nutredairy/";
        if (Product.Archivo != null && Product.Archivo.Length > 0)
        {
            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Product.Archivo.FileName);
            var rutaDestino = "ftp"+ ruta + nombreArchivo;
            var solicitud = (System.Net.FtpWebRequest)WebRequest.Create(rutaDestino);
            solicitud.Credentials = new NetworkCredential("nutredairy", "123456");
            solicitud.Method = WebRequestMethods.Ftp.UploadFile;
            using (var stream = Product.Archivo.OpenReadStream())
                using (var reader = new BinaryReader(stream))
                {
                    var contenido = reader.ReadBytes((int)Product.Archivo.Length);
                    using (var ftpStream = solicitud.GetRequestStream())
                    {
                        ftpStream.Write(contenido, 0, contenido.Length);
                    }
                }
            // Actualizar el modelo con la URL de la imagen en el servidor FTP
            var rutaAcceso = "http"+ ruta + nombreArchivo;
            Product.ImagePath = rutaAcceso;
        }

        if(id == Product.Id)
        {
            _context.Products.Update(Product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View("Detail",Product);
    }

    public IActionResult Delete(int id)
    {
        var data = _context.Products.FirstOrDefault(a=> a.Id == id);
        if (data == null) { return View("NotFound"); }
        return View(data);
    }

    [HttpPost]
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
            return RedirectToAction(nameof(Index),"Admin");
        }
        return View("Delete",Product);
    }
}
