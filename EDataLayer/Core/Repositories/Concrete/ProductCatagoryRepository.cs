using EDataLayer.Core.DataContext;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using EDataLayer.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDataLayer.Core.Repositories.Concrete
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {

        private readonly EDataServeContext _context;
        public ProductCategoryRepository(EDataServeContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategoryWithProductResult> ProductCategoriesWithProducts()
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
                          }).OrderBy(pc => pc.ProductCategoryName).ToList();

            return joined;
        }

        public IEnumerable<ProductCategoryWithProductResult> ProductCategoryWithProducts(ProductCategory productCategory)
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
                          }).OrderBy(p => productCategory.CategoryName).ToList();

            return joined;
        }

        public bool ProductCategoryExists(ProductCategory productCategory)
        {
            return _context.ProductCategories.Count(pc => pc.ProductCategoryId == productCategory.ProductCategoryId) > 0;
        }

        public bool ProductCategoryRangeExists(IEnumerable<ProductCategory> productCategories)
        {
            var matchingCount = productCategories
                 .Where(pc1 => _context.ProductCategories
                 .Any(pc2 => pc2.ProductCategoryId == pc1.ProductCategoryId)).Count();

            return matchingCount == productCategories.Count();
        }
    }
}

    

