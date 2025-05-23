using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    
    [ValidateNever]
    public int? CompanyId { get; set; }
    
    [ValidateNever]
    public Company? Company { get; set; }
    
    [ValidateNever]
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }

    [ValidateNever] 
    public ICollection<OrderHeader> OrderHeaders { get; set; }

    [NotMapped]
    public string Role { get; set; }
}