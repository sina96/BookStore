using BookStore_razor_temp.Data;
using BookStore_razor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_razor_temp.Pages.Categories;

[BindProperties]
public class Edit : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    public Category? Category { get; set; }

    public Edit(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet(int? id)
    {
        if (id is > 0)
        {
            Category = _dbContext.Categories.Find(id);
        }
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid && Category != null)
        {
            _dbContext.Categories.Update(Category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}