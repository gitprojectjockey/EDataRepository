using EDataLayer.Core.DataContext;
using EDataLayer.Core.EUnitOfWork.Async.Abstract;
using EDataLayer.Core.Repositories.Async.Abstract;
using EDataLayer.Core.Repositories.Async.Concrete;
using System;
using System.Threading.Tasks;

namespace EDataLayer.Core.EUnitOfWork.Async.Concrete
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly EDataServeContext _context;
        public UnitOfWorkAsync(EDataServeContext context)
        {
            _context = context;
            Products = new ProductRepositoryAsync(_context);
            Companies = new CompanyRepositoryAsync(_context);
            ProductCategories = new ProductCategoryRepositoryAsync(_context);
        }

        public IProductRepositoryAsync Products { get; private set; }

        public ICompanyRepositoryAsync Companies { get; private set; }

        public IProductCategoryRepositoryAsync ProductCategories { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

    }
}
