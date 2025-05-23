using System.Linq.Expressions;

namespace BookStore_DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracking = false);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}