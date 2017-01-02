using EDataLayer.Core.DataContext;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using EDataLayer.Core.Repositories.Async.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EDataLayer.Core.Repositories.Async.Concrete
{
    public class ProductRepositoryAsync : RepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly EDataServeContext _context;
        public ProductRepositoryAsync(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> ProductsByCompanyAndProductCategoryAsync(Company company, ProductCategory category)
        {
            return await _context.Products
                .Where(p => p.CompanyId == company.CompanyId && p.ProductCategoryId == category.ProductCategoryId)
                .OrderBy(p => p.ProductName)
                .ToListAsync() as IEnumerable<Product>;
        }

        public async Task<bool> ProductExistsAsync(Product product)
        {
            return await _context.Products.CountAsync(
                p => p.ProductName == product.ProductName && p.CompanyId == product.CompanyId && p.ProductCategoryId == product.ProductCategoryId) > 0;
        }

        public async Task<bool> ProductRangeExistsAsync(IEnumerable<Product> products)
        {
            Task<bool> countsMatch = Task.Factory.StartNew(() => products
               .Where(p1 => _context.Products
               .Any(p2 => p2.ProductName == p1.ProductName && p2.CompanyId == p1.CompanyId && p2.ProductCategoryId == p1.ProductCategoryId)).Count() == products.Count());

            return await countsMatch;
        }

        public async Task<IEnumerable<Product>> GetProductsByRangeAsync(IEnumerable<Product> products)
        {
            var productIds = products.Select(p => p.ProductId).ToArray();
            return await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync() as IEnumerable<Product>;
        }

        public async Task<IEnumerable<Product>> GetProductsByRangeAsync(IEnumerable<Guid> productIds)
        {
            return await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync() as IEnumerable<Product>;
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.ProductName == name) as Product;
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText)
        {
            return await _context.PagedProducts(displayLength, displayStart, sortColumn, sortDirection, searchText);
            
        }

        public async Task<IEnumerable<CompanyWithProductResult>> GetPagedProductsByCompanyAsync(string companyName, int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText)
        {
            return await _context.PagedProductsByCompany(companyName, displayLength, displayStart, sortColumn, sortDirection, searchText);
        }
    }
}
