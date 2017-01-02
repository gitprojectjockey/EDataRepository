using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDataLayer.Core.Repositories.Async.Abstract
{
    public interface IProductCategoryRepositoryAsync : IRepositoryAsync<ProductCategory>
    {
        Task<IEnumerable<ProductCategoryWithProductResult>> ProductCategoriesWithProducts();

        Task<IEnumerable<ProductCategoryWithProductResult>> ProductCategoryWithProducts(ProductCategory productCategory);

        Task<bool> ProductCategoryExists(ProductCategory productCategory);

        Task<bool> ProductCategoryRangeExists(IEnumerable<ProductCategory> productCategories);
    }
}
