using System;


namespace EDataLayer.Core.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public Guid ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory  { get; set; }
    }
}
