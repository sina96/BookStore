using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore_Models;

public class Category
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    [Range(1,100, ErrorMessage = "Please enter a value between 1 and 100")]
    public int DisplayOrder { get; set; }
    
    [ValidateNever]
    public ICollection<Product> Products { get; set; }
}