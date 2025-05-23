using System.Linq.Expressions;
using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStore_DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<T> DbSet;
    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        this.DbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
    {
        IQueryable<T> query = DbSet;
        if (filter != null)
        {
          query = query.Where(filter);  
        }
        
        if (includeProperties != null)
        {
            query = includeProperties
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query,
                    (current, includeProp) => current.Include(includeProp.Trim()));
        }

        return query.ToList();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracking = false)
    {
        IQueryable<T> query = tracking? DbSet : DbSet.AsNoTracking();
        query = query.Where(filter);

        if (includeProperties != null)
        {
            query = includeProperties
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query,
                    (current, includeProp) => current.Include(includeProp.Trim()));
        }

        return query.FirstOrDefault();
        
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }
}