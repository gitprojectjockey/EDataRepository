using EDataLayer.Core.Repositories.Async.Abstract;
using System;
using System.Threading.Tasks;

namespace EDataLayer.Core.EUnitOfWork.Async.Abstract
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        IProductRepositoryAsync Products { get; }

        ICompanyRepositoryAsync Companies { get; }

        IProductCategoryRepositoryAsync ProductCategories{ get; }

        Task<int> CompleteAsync();
    }
}
