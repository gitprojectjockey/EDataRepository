using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System.Collections.Generic;

namespace EDataLayer.Core.Repositories.Abstract
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategoryWithProductResult> ProductCategoriesWithProducts();

        IEnumerable<ProductCategoryWithProductResult> ProductCategoryWithProducts(ProductCategory productCategory);

        bool ProductCategoryExists(ProductCategory productCategory);

        bool ProductCategoryRangeExists(IEnumerable<ProductCategory> ProductCategories);
    }
}
