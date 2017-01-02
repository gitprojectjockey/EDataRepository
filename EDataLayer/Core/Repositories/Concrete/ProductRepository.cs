using System.Collections.Generic;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Repositories.Abstract;
using EDataLayer.Core.DataContext;
using System.Linq;
using System;

namespace EDataLayer.Core.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly EDataServeContext _context;

        public ProductRepository(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> ProductsByCompanyAndProductCategory(Company company, ProductCategory category)
        {
            return _context.Products
                .Where(p => p.CompanyId == company.CompanyId && p.ProductCategoryId == category.ProductCategoryId)
                .OrderBy(p => p.ProductName)
                .ToList() as IEnumerable<Product>;
        }

        public bool ProductExists(Product product)
        {
            return _context.Products
                .Count(p => p.ProductName == product.ProductName && p.CompanyId == product.CompanyId && p.ProductCategoryId == product.ProductCategoryId ) > 0;
        }

        public bool ProductRangeExists(IEnumerable<Product> products)
        {
            var matchingCount = products
                 .Where(p1 => _context.Products
                 .Any(p2 => p2.ProductName == p1.ProductName && p2.CompanyId == p1.CompanyId && p2.ProductCategoryId == p1.ProductCategoryId)).Count();
            
           return matchingCount == products.Count();
         }

        public IEnumerable<Product> GetProductsByRange(IEnumerable<Product> products)
        {
            var productIds = products.Select(p => p.ProductId).ToArray();
            return _context.Products.Where(p => productIds.Contains(p.ProductId)).ToList() as IEnumerable<Product>;
        }

        public IEnumerable<Product> GetProductsByRange(IEnumerable<Guid> productIds)
        {
            return _context.Products.Where(p => productIds.Contains(p.ProductId)).ToList() as IEnumerable<Product>;
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.SingleOrDefault(p => p.ProductName == name) as Product;
        }
    }
}
