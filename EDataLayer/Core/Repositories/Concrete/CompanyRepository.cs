using EDataLayer.Core.DataContext;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Repositories.Abstract;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace EDataLayer.Core.Repositories.Concrete
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly EDataServeContext _context;
        public CompanyRepository(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<CompanyWithProductResult> CompaniesWithProducts()
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
                          }).OrderBy(c => c.CompanyName).ToList();

            return joined;
        }

        public IEnumerable<CompanyWithProductResult> CompanyWithProducts(Company company)
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
                          }).OrderBy(p => p.ProductName).ToList();

            return joined;
        }

        public bool CompanyExists(Company company)
        {
            return _context.Companies.Count(pc => pc.CompanyId== company.CompanyId) > 0;
        }

        public bool CompanyRangeExists(IEnumerable<Company> companies)
        {
            var matchingCount = companies
                 .Where(c1 => _context.Companies
                 .Any(c2 => c2.CompanyId == c1.CompanyId)).Count();

            return matchingCount == companies.Count();
        }

    }
}
