using EDataLayer.Core.EUnitOfWork.Abstract;
using System;
using EDataLayer.Core.Repositories.Abstract;
using EDataLayer.Core.DataContext;
using EDataLayer.Core.Repositories.Concrete;


namespace EDataLayer.Core.EUnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly EDataServeContext _context;

        public UnitOfWork(EDataServeContext context)
        {
            _context = context;
            Companies = new CompanyRepository(_context);
            Products = new ProductRepository(_context);
            ProductCategories = new ProductCategoryRepository(_context);
        }

        public ICompanyRepository Companies { get; private set; }

        public IProductRepository Products{ get; private set; }

        public IProductCategoryRepository ProductCategories { get; private set; }

        public int Complete()
        {
           return _context.SaveChanges();
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
