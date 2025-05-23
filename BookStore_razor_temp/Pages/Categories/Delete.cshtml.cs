using BookStore_razor_temp.Data;
using BookStore_razor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_razor_temp.Pages.Categories;
[BindProperties]
public class Delete : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public Category? Category { get; set; }

    public Delete(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet(int? id)
    {
        if (id != null && id > 0)
        {
            Category = _dbContext.Categories.Find(Category.Id);
        }
    }
    public IActionResult onPost()
    {
        if (Category is null)
        {
            Category = _dbContext.Categories.Find(Category.Id);
        }
        if (ModelState.IsValid && Category != null)
        {
            _dbContext.Categories.Remove(Category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

        return Page();;
    }
}