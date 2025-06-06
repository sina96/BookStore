using BookStore_razor_temp.Models;
using Microsoft.EntityFrameworkCore;
namespace BookStore_razor_temp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(cat => cat.Id);
        modelBuilder.Entity<Category>().Property(cat => cat.Name).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<Category>().HasData(
            new Category{Id = 1, Name = "Action", DisplayOrder = 1},
            new Category{Id = 2, Name = "SciFi", DisplayOrder = 2},
            new Category{Id = 3, Name = "History", DisplayOrder = 3},
            new Category{Id = 4, Name = "Romance", DisplayOrder = 4}
        );
        base.OnModelCreating(modelBuilder);
    }
}