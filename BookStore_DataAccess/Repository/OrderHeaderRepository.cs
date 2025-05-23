using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;

namespace BookStore_DataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private ApplicationDbContext _dbContext;
    public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(OrderHeader orderHeader)
    {
        _dbContext.OrderHeaders.Update(orderHeader);
    }

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderHeaderFromDb = _dbContext.OrderHeaders.FirstOrDefault(oh => oh.Id == id);
        if (orderHeaderFromDb != null)
        {
            orderHeaderFromDb.OrderStatus = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orderHeaderFromDb.PaymentStatus = paymentStatus;
            }
        }
    }

    public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
    {
        var orderHeaderFromDb = _dbContext.OrderHeaders.FirstOrDefault(oh => oh.Id == id);
        if (!string.IsNullOrEmpty(sessionId))
        {
            orderHeaderFromDb.SessionId = sessionId;
        }
        
        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            orderHeaderFromDb.PaymentIntentId = paymentIntentId;
            orderHeaderFromDb.PaymentDate = DateTime.UtcNow;
        }
    }
}