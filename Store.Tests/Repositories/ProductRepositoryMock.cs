using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class ProductRepositoryMock : IProductRepository
    {
        private readonly ICollection<Product> _products;

        public ProductRepositoryMock()
        {
            _products = new List<Product>();
            LoadFakeProducts();
        }

        private void LoadFakeProducts()
        {

            /// Active products
            for (var i = 0; i < 3; i++)
            {
                var product = new Product($"Product active {i}", 10, true);
                _products.Add(product);
            }

            /// Inactive products
            for (var i = 0; i < 2; i++)
            {
                var product = new Product($"Product inactive {i}", 10, false);
                _products.Add(product);
            }
        }

        public IEnumerable<Product> Get(IEnumerable<Guid> ids) 
        {
            var products =  new List<Product>();
            foreach(var id in ids)
            {
                var product = new Product($"Product {id}", 10, true);
                product.Id = id;
                products.Add(product);
            }

            return products;
        }
        public IEnumerable<Product> GetAll() => _products;

    }
}
