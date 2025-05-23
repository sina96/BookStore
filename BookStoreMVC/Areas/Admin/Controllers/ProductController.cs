using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;
using BookStore_Models.ViewModels;
using BookStore_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.RoleAdmin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var products = _unitOfWork.Product.GetAll(null, includeProperties: "Category").ToList();
        return View(products);
    }

    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> categories = _unitOfWork.Category
            .GetAll(null)
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

        //ViewBag.CategoryList = categories;
        //ViewData["CategoryData"] = categories;
        ProductViewModel productViewModel = new()
        {
            CategoryList = categories,
            Product = (id == null || id == 0) ? new Product() : _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id),
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"/images/products");

                if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('/'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var stream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                productViewModel.Product.ImageUrl = @"/images/products/" + fileName;
            }
            else
            {
                productViewModel.Product.ImageUrl = "";
            }

            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productViewModel.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productViewModel.Product);
            }

            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }

        productViewModel.CategoryList = _unitOfWork.Category.GetAll(null)
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

        return View(productViewModel);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _unitOfWork.Product.GetAll(null, includeProperties: "Category").ToList();
        return Json(new { data = products });
    }
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Product not found" });
        }
        
        if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath,
                productToBeDeleted.ImageUrl.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
        
        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Product deleted successfully" });
    }

    #endregion
}