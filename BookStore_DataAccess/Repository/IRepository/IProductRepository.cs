using BookStore_Models;

namespace BookStore_DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
}