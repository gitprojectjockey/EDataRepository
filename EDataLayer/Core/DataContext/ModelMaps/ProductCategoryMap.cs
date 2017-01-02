using System.Data.Entity.ModelConfiguration;
using EDataLayer.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace EDataLayer.Core.DataContext.ModelMaps
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            //Primary Key
            ToTable("ProductCategory")
              .HasKey(pc => pc.ProductCategoryId)
              .Property(pc => pc.ProductCategoryId)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Properties
            Property(pc => pc.CategoryName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
