using BookStore_razor_temp.Data;
using BookStore_razor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_razor_temp.Pages.Categories;
[BindProperties]
public class Create : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public Category Category { get; set; }

    public Create(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        _dbContext.Categories.Add(Category);
        _dbContext.SaveChanges();
        TempData["success"] = "Category created successfully";
        return RedirectToPage("Index");
    }
}