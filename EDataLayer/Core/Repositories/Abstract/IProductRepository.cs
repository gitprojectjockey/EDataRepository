using EDataLayer.Core.Domain;
using System;
using System.Collections.Generic;

namespace EDataLayer.Core.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> ProductsByCompanyAndProductCategory(Company company,ProductCategory productCategory);

        bool ProductExists(Product product);

        bool ProductRangeExists(IEnumerable<Product> products);

        IEnumerable<Product> GetProductsByRange(IEnumerable<Product> products);

        IEnumerable<Product> GetProductsByRange(IEnumerable<Guid> productIds);

        Product GetProductByName(string name);
    }
}
