using BookStore_DataAccess.Data;
using BookStore_DataAccess.Repository.IRepository;
using BookStore_Models;

namespace BookStore_DataAccess.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private ApplicationDbContext _dbContext;
    public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Company company)
    {
        _dbContext.Companies.Update(company);
    }
}