using PAzIGProj.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAzIGProj.Services
{
    public interface IProductManager
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);
    }
}
