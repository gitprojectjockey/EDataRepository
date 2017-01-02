using EDataLayer.Core.DataContext.ModelMaps;
using EDataLayer.Core.Domain;
using EDataLayer.Core.Domain.ResultEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EDataLayer.Core.DataContext
{
    public class EDataServeContext : DbContext
    {
        public EDataServeContext() : base("name=EDataServeContext")
        {
            //Database.SetInitializer<EDataServeContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EDataServeContext>());
           
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public async Task<IEnumerable<Product>> PagedProducts(int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText)
        {
            return await Products.SqlQuery("spGetPagedProducts @DisplayLength, @DisplayStart, @SortCol, @SortDir, @Search",
                new SqlParameter() { ParameterName = "DisplayLength", SqlDbType = SqlDbType.Int, Value = displayLength },
                new SqlParameter() { ParameterName = "DisplayStart", SqlDbType = SqlDbType.Int, Value = displayStart },
                new SqlParameter() { ParameterName = "SortCol", SqlDbType = SqlDbType.Int, Value = sortColumn },
                new SqlParameter() { ParameterName = "SortDir", SqlDbType = SqlDbType.NVarChar, Value = sortDirection },
                new SqlParameter() { IsNullable = true, ParameterName = "Search", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrWhiteSpace(searchText) ? DBNull.Value : (object)searchText }
                ).ToListAsync();
        }

        public async Task<IEnumerable<CompanyWithProductResult>> PagedProductsByCompany(string companyName, int displayLength, int displayStart, int sortColumn, string sortDirection, string searchText)
        {
            return await Database.SqlQuery<CompanyWithProductResult>("spGetPagedProductsByCompany @CompanyName, @DisplayLength, @DisplayStart, @SortCol, @SortDir, @Search",
                new SqlParameter() { ParameterName = "CompanyName", SqlDbType = SqlDbType.NVarChar, Value = companyName },
                new SqlParameter() { ParameterName = "DisplayLength", SqlDbType = SqlDbType.Int, Value = displayLength },
                new SqlParameter() { ParameterName = "DisplayStart", SqlDbType = SqlDbType.Int, Value = displayStart },
                new SqlParameter() { ParameterName = "SortCol", SqlDbType = SqlDbType.Int, Value = sortColumn },
                new SqlParameter() { ParameterName = "SortDir", SqlDbType = SqlDbType.NVarChar, Value = sortDirection },
                new SqlParameter() { IsNullable = true, ParameterName = "Search", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrWhiteSpace(searchText) ? DBNull.Value : (object)searchText }
                ).ToListAsync();
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
