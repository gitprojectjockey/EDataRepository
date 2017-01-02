using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EDataLayer.Core.Domain;
using System;

namespace EDataLayer.Core.DataContext.ModelMaps
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            ToTable("Company")
                .HasKey(c => c.CompanyId)
                .Property(c => c.CompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(50);


            Property(c => c.State)
              .IsRequired()
              .HasMaxLength(2);
        }
    }
}
