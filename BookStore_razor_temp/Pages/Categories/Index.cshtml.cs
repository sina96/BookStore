using BookStore_razor_temp.Data;
using BookStore_razor_temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_razor_temp.Pages.Categories;

public class Index : PageModel
{
    
    private readonly ApplicationDbContext _dbContext;
    public List<Category> Categories { get; set; }

    public Index(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        Categories = _dbContext.Categories.ToList();
    }
}