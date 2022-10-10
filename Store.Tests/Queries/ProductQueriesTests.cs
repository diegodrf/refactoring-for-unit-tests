using Store.Domain.Queries;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private readonly IProductRepository _productRepository = new ProductRepositoryMock();

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
            var products = _productRepository.GetAll();
            var total = products.AsQueryable()
                .Where(ProductQueries.GetActiveProducts())
                .Count();

            Assert.AreEqual(3, total);


        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var products = _productRepository.GetAll();
            var total = products.AsQueryable()
                .Where(ProductQueries.GetInactiveProducts())
                .Count();

            Assert.AreEqual(2, total);
        }

    }
}
