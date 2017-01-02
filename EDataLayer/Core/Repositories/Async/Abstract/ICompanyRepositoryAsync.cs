using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDataLayer.Core.Repositories.Async.Abstract
{
    public interface ICompanyRepositoryAsync : IRepositoryAsync<Company>
    {
        Task<IEnumerable<CompanyWithProductResult>> CompaniesWithProducts();

        Task<IEnumerable<CompanyWithProductResult>> CompanyWithProducts(Company company);

        Task<bool> CompanyExists(Company company);

        Task<bool> CompanyRangeExists(IEnumerable<Company> companies);
    }
}
