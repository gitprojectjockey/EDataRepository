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
    public class ProductCategoryRepositoryAsync : RepositoryAsync<ProductCategory>, IProductCategoryRepositoryAsync
    {
        private readonly EDataServeContext _context;
        public ProductCategoryRepositoryAsync(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategoryWithProductResult>> ProductCategoriesWithProducts()
        {
            var joined = (from p in _context.Products
                          join pc in _context.ProductCategories
                          on p.ProductCategoryId equals pc.ProductCategoryId
                          select new ProductCategoryWithProductResult()
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              ProductDescription = p.Description,
                              Price = p.Price,
                              ProductCategoryName = pc.CategoryName,
                              ProductCategoryId = pc.ProductCategoryId
                          }).OrderBy(pc => pc.ProductCategoryName).ToListAsync();

            return await joined;
        }

        public async Task<IEnumerable<ProductCategoryWithProductResult>> ProductCategoryWithProducts(ProductCategory productCategory)
        {
            var joined = (from p in _context.Products
                          join pc in _context.ProductCategories
                          on p.ProductCategoryId equals pc.ProductCategoryId
                          where pc.ProductCategoryId == productCategory.ProductCategoryId
                          select new ProductCategoryWithProductResult()
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              ProductDescription = p.Description,
                              Price = p.Price,
                              ProductCategoryName = pc.CategoryName,
                              ProductCategoryId = pc.ProductCategoryId
                          }).OrderBy(p => productCategory.CategoryName).ToListAsync();

            return await joined;
        }

        public async Task<bool> ProductCategoryExists(ProductCategory productCategory)
        {
            return await _context.ProductCategories.CountAsync(pc => pc.ProductCategoryId == productCategory.ProductCategoryId) > 0;
        }

        public async Task<bool> ProductCategoryRangeExists(IEnumerable<ProductCategory> productCategories)
        {
            Task<bool> countsMatch = Task.Factory.StartNew(() => productCategories
                .Where(pc1 => _context.ProductCategories
                .Any(pc2 => pc2.ProductCategoryId == pc1.ProductCategoryId)).Count() == productCategories.Count());

            return await countsMatch;
        }
    }
}
