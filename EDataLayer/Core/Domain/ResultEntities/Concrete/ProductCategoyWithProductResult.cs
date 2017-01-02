using System;

namespace EDataLayer.Core.Domain.ResultEntities.Concrete
{
    public class ProductCategoryWithProductResult
    {
        public string ProductName { get; set; }

        public Guid ProductId{ get; set; }

        public decimal Price { get; set; }

        public string ProductDescription { get; set; }

        public string ProductCategoryName { get; set; }

        public Guid ProductCategoryId { get; set; }
    }
}
