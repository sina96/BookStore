using System.ComponentModel.DataAnnotations;

namespace BookStore_razor_temp.Models;

public class Category
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    [Range(1,100, ErrorMessage = "Please enter a value between 1 and 100")]
    public int DisplayOrder { get; set; }
}