
using System;
using System.Collections.Generic;


namespace EDataLayer.Core.Domain
{
    public class ProductCategory 
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        public Guid ProductCategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection <Product>  Products { get; set; }
    }
}
