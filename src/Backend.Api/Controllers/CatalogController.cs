// using Backend.Api.Interfaces;
// using Backend.Api.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace Backend.Api.Controllers;

// public sealed class CatalogController : Controller
// {
//     private readonly IProductService _service;

//     public CatalogController(IProductService service)
//     {
//         _service=service;
//     }

//     [HttpGet("/catalog")]
//     public IActionResult Index()
//     {
//         var products = _service.GetAll();
//         return View(products);
//     }

//     [HttpDelete("{productCode}")]
//     public ActionResult Delete(Product product)
//     {
        
//         ViewData = _service.Create(product);
//         return View();
//     }

//     // public ActionResult Details(string? productCode)
//     // {
//     //     if (id == null)
//     //     {
//     //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//     //     }
//     //     Product product = _service.GetByCode(productCode);

//     //     if (product == null)
//     //     {
//     //         return HttpNotFound();
//     //     }
//     //     return View(product);
//     // }

// }