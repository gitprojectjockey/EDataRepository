using System;

namespace EDataLayer.Core.Domain.ResultEntities.Concrete
{
    public class CompanyWithProductResult
    {

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public Guid CompanyId { get; set; }

        public Guid ProductCategoryId { get; set; }


    }
}
