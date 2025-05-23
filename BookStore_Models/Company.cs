using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    
    [ValidateNever]
    public ICollection<ApplicationUser> ApplicationUsers { get; set; }
}