using EDataLayer.Core.DataContext;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using EDataLayer.Core.Repositories.Async.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EDataLayer.Core.Repositories.Async.Concrete
{
    public class CompanyRepositoryAsync : RepositoryAsync<Company>, ICompanyRepositoryAsync
    {
        private readonly EDataServeContext _context;
        public CompanyRepositoryAsync(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyWithProductResult>> CompaniesWithProducts()
        {
            var joined = (from p in _context.Products
                          join c in _context.Companies
                          on p.CompanyId equals c.CompanyId
                          select new CompanyWithProductResult()
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              Description = p.Description,
                              Price = p.Price,
                              CompanyName = c.CompanyName,
                              CompanyId = c.CompanyId
                          }).OrderBy(c => c.CompanyName).ToListAsync();

            return await joined;
        }

        public async Task<IEnumerable<CompanyWithProductResult>> CompanyWithProducts(Company company)
        {
            var joined = (from p in _context.Products
                          join c in _context.Companies
                          on p.CompanyId equals c.CompanyId
                          where c.CompanyId == company.CompanyId
                          select new CompanyWithProductResult()
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              Description = p.Description,
                              Price = p.Price,
                              CompanyName = c.CompanyName,
                              CompanyId = c.CompanyId
                          }).OrderBy(p => p.ProductName).ToListAsync();

            return await joined;
        }

        public async Task<bool> CompanyExists(Company company)
        {
            return await _context.Companies.CountAsync(pc => pc.CompanyId == company.CompanyId) > 0;
        }

        public async Task<bool> CompanyRangeExists(IEnumerable<Company> companies)
        {
            Task<bool> countsMatch = Task.Factory.StartNew(() => companies
               .Where(c1 => _context.Companies
               .Any(c2 => c2.CompanyId == c1.CompanyId)).Count() == companies.Count());

            return await countsMatch;
        }
    }
}
