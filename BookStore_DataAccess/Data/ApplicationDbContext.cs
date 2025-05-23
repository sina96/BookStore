using BookStore_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore_DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(cat => cat.Id);
        modelBuilder.Entity<Category>().Property(cat => cat.Name).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "History", DisplayOrder = 3 },
            new Category { Id = 4, Name = "Romance", DisplayOrder = 4 }
        );
        

        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Title).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.ISBN).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Author).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.ListPrice).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Price50).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Price100).IsRequired();
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) // Product has one Category
            .WithMany(c => c.Products) // Category has many Products
            .HasForeignKey(p => p.CategoryId) // FK in Product
            .OnDelete(DeleteBehavior.Restrict); // Optional: cascade on delete
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Fortune of Time",
                Author = "Billy Spark",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SWD9999001",
                ListPrice = 99,
                Price = 90,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product
            {
                Id = 2,
                Title = "Dark Skies",
                Author = "Nancy Hoover",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "CAW777777701",
                ListPrice = 40,
                Price = 30,
                Price50 = 25,
                Price100 = 20,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product
            {
                Id = 3,
                Title = "Vanish in the Sunset",
                Author = "Julian Button",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "RITO5555501",
                ListPrice = 55,
                Price = 50,
                Price50 = 40,
                Price100 = 35,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product
            {
                Id = 4,
                Title = "Cotton Candy",
                Author = "Abby Muscles",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "WS3333333301",
                ListPrice = 70,
                Price = 65,
                Price50 = 60,
                Price100 = 55,
                CategoryId = 2,
                ImageUrl = ""
            },
            new Product
            {
                Id = 5,
                Title = "Rock in the Ocean",
                Author = "Ron Parker",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SOTJ1111111101",
                ListPrice = 30,
                Price = 27,
                Price50 = 25,
                Price100 = 20,
                CategoryId = 2,
                ImageUrl = ""
            },
            new Product
            {
                Id = 6,
                Title = "Leaves and Wonders",
                Author = "Laura Phantom",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "FOT000000001",
                ListPrice = 25,
                Price = 23,
                Price50 = 22,
                Price100 = 20,
                CategoryId = 3,
                ImageUrl = ""
            }
        );

        modelBuilder.Entity<ApplicationUser>().Property(u => u.Name).IsRequired();
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Company) 
            .WithMany(c => c.ApplicationUsers) 
            .HasForeignKey(u => u.CompanyId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<Company>().HasKey(c => c.Id);
        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "Tech Solution", StreetAddress="123 Tech St", City="Tech City",
                PostalCode="12121", State="IL", PhoneNumber="6669990000"},
            new Company {
                Id = 2,
                Name = "Vivid Books",
                StreetAddress = "999 Vid St",
                City = "Vid City",
                PostalCode = "66666",
                State = "IL",
                PhoneNumber = "7779990000"
            },
            new Company {
                Id = 3,
                Name = "Readers Club",
                StreetAddress = "999 Main St",
                City = "Lala land",
                PostalCode = "99999",
                State = "NY",
                PhoneNumber = "1113335555"
            }
        );
        
        modelBuilder.Entity<ShoppingCart>().HasKey(c => c.Id);
        modelBuilder.Entity<ShoppingCart>()
            .HasOne(c => c.ApplicationUser) 
            .WithMany(u => u.ShoppingCarts) 
            .HasForeignKey(c => c.ApplicationUserId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<ShoppingCart>()
            .HasOne(c => c.Product) 
            .WithMany(p => p.ShoppingCarts) 
            .HasForeignKey(c => c.ProductId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<OrderHeader>().HasKey(c => c.Id);
        modelBuilder.Entity<OrderHeader>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<OrderHeader>().Property(c => c.PhoneNumber).IsRequired();
        modelBuilder.Entity<OrderHeader>().Property(c => c.StreetAddress).IsRequired();
        modelBuilder.Entity<OrderHeader>().Property(c => c.City).IsRequired();
        modelBuilder.Entity<OrderHeader>().Property(c => c.State).IsRequired();
        modelBuilder.Entity<OrderHeader>().Property(c => c.PostalCode).IsRequired();
        modelBuilder.Entity<OrderHeader>()
            .HasOne(o => o.ApplicationUser) 
            .WithMany(u => u.OrderHeaders) 
            .HasForeignKey(o => o.ApplicationUserId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<OrderDetail>().HasKey(c => c.Id);
        modelBuilder.Entity<OrderDetail>().Property(c => c.OrderHeaderId).IsRequired();
        modelBuilder.Entity<OrderDetail>().Property(c => c.ProductId).IsRequired();
        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.OrderHeader)
            .WithOne(o => o.OrderDetail)
            .HasForeignKey<OrderDetail>(o => o.OrderHeaderId)
            .IsRequired();

        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Product) 
            .WithMany(u => u.OrderDetails) 
            .HasForeignKey(o => o.ProductId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        base.OnModelCreating(modelBuilder);
    }
}