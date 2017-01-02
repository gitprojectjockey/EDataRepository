using System.Data.Entity.ModelConfiguration;
using EDataLayer.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Data.Entity.Infrastructure.Annotations;

namespace EDataLayer.Core.DataContext.ModelMaps
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary key
            ToTable("Product")
               .HasKey(p => p.ProductId)
               .Property(p => p.ProductId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Create unique compsite key (name,companyId,productCategoryId) to prevent duplicate entries
            Property(p => p.ProductName)
                     .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Product", 1) { IsUnique = true }));

            Property(p => p.CompanyId)
                     .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Product", 2) { IsUnique = true }));

            Property(p => p.ProductCategoryId)
                     .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Product",3) { IsUnique = true }));

            // Properties
            Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Description)
              .IsRequired()
              .HasMaxLength(500);

            Property<decimal>(p => p.Price)
                .IsRequired();

            Property<Guid>(p => p.CompanyId)
               .IsRequired();

            Property<Guid>(p => p.ProductCategoryId)
              .IsRequired();

            // One Company to Many Products Relationship
            HasRequired(p => p.Company)
                .WithMany(p => p.Products)
                .HasForeignKey<Guid>(p => p.CompanyId)
                .WillCascadeOnDelete(false);

            // One Product Category to Many Product Relationship
            HasRequired(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey<Guid>(p => p.ProductCategoryId)
                .WillCascadeOnDelete(false);

        }
    }
}
