using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;

namespace BookStore_DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private ApplicationDbContext _dbContext;
    public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(ShoppingCart shoppingCart)
    {
        _dbContext.ShoppingCarts.Update(shoppingCart);
    }
}