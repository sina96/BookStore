using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;

namespace BookStore_DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _dbContext;
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Product product)
    {
        var objFromDb = _dbContext.Products.FirstOrDefault(x => x.Id == product.Id);
        if (objFromDb != null)
        {
            objFromDb.Title = product.Title;
            objFromDb.ISBN = product.ISBN;
            objFromDb.Description = product.Description;
            objFromDb.Price = product.Price;
            if (product.ImageUrl != null)
            {
                objFromDb.ImageUrl = product.ImageUrl;
            }
            objFromDb.Author = product.Author;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.ListPrice = product.ListPrice;
            objFromDb.Price50 = product.Price50;
            objFromDb.Price100 = product.Price100;
        }
        //_dbContext.Update(product);
    }
}