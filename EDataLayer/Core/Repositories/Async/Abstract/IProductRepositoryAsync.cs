using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDataLayer.Core.Repositories.Async.Abstract
{
    public interface IProductRepositoryAsync : IRepositoryAsync<Product>
    {
        Task<IEnumerable<Product>> ProductsByCompanyAndProductCategoryAsync(Company company,ProductCategory productCategory);

        Task<bool> ProductExistsForUpdateAsync(Product product);

        Task<bool> ProductExistsForCreateAsync(Product product);

        Task<bool> ProductRangeExistsForUpdateAsync(IEnumerable<Product> products);

        Task<bool> ProductRangeExistsForCreateAsync(IEnumerable<Product> products);

        Task<IEnumerable<Product>> GetProductsByRangeAsync(IEnumerable<Product> products);

        Task<IEnumerable<Product>> GetProductsByRangeAsync(IEnumerable<Guid> productIds);

        Task<Product> GetProductByNameAsync(string name);

        Task<IEnumerable<Product>> GetPagedProductsAsync(int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText);

        Task<IEnumerable<CompanyWithProductResult>> GetPagedProductsByCompanyAsync(string companyName, int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText);
    }
}
