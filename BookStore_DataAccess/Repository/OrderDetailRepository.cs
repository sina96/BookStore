using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;

namespace BookStore_DataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private ApplicationDbContext _dbContext;
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(OrderDetail orderDetail)
    {
        _dbContext.OrderDetails.Update(orderDetail);
    }
}