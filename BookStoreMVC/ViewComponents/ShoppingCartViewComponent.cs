using System.Security.Claims;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Utility;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.ViewComponents;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
            {
                var userId = claim.Value;
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart
                        .GetAll(c => c.ApplicationUserId == userId).Count());
            }
            return View((int)HttpContext.Session.GetInt32(SD.SessionCart)!);
        }
        else
        {
            HttpContext.Session.Clear();
            return View(0);
        }
    }
}