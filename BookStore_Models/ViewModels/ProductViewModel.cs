using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models.ViewModels;

public class ProductViewModel
{
   public Product Product { get; set; } 
   [ValidateNever]
   public IEnumerable<SelectListItem> CategoryList { get; set; }
}