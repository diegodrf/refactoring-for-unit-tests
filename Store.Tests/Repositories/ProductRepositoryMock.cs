using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class ProductRepositoryMock : IProductRepository
    {
        private int _id = 0;

        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {

            var products = new List<Product>();
            for(var i = 0; i < 3; i++)
            {
                var product = new Product($"Product 0{GetNextId()}", 10, true);
                products.Add(product);
            }
            for (var i = 0; i < 2; i++)
            {
                var product = new Product($"Product 0{GetNextId()}", 10, false);
                products.Add(product);
            }

            return products;
        }

        private int GetNextId() => ++_id;
    }
}
