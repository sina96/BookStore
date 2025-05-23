using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    
    [Display(Name = "List Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }
    [Display(Name = "Price 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }
    [Display(Name = "Price 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }
    [Display(Name = "Price 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }
    
    [ValidateNever]
    public int CategoryId { get; set; }
    
    [ValidateNever]
    public Category Category { get; set; }
    
    [ValidateNever]
    public string ImageUrl { get; set; }
    
    [ValidateNever]
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    [ValidateNever]
    public ICollection<OrderDetail> OrderDetails { get; set; }

    
}