using System.Diagnostics;
using System.Security.Claims;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;
using BookStore_Utility;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        
        IEnumerable<Product> products = _unitOfWork.Product.GetAll(null, includeProperties: "Category").ToList();

        return View(products);
    }

    public IActionResult Details(int id)
    {
        ShoppingCart cart = new()
        {
            Product = _unitOfWork.Product
                .GetFirstOrDefault(product => product.Id == id, includeProperties: "Category"),
            ProductId = id,
            Count = 1
        };

        if (cart != null)
        {
            return View(cart);
        }

        return NotFound();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCart cartFromDb = _unitOfWork.ShoppingCart
            .GetFirstOrDefault(c => c.ApplicationUserId == userId && c.ProductId == shoppingCart.ProductId);

        if (cartFromDb != null)
        {
            //cart exists
            cartFromDb.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
        }
        else
        {
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.ShoppingCart
                    .GetAll(c => c.ApplicationUserId == userId).Count());
        }

        TempData["success"] = "Cart has been submitted.";

        if (userId != null)
        {
            shoppingCart.ApplicationUserId = userId;
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        return NotFound();
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