using Store.Domain;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class DiscountRepositoryMock : IDiscountRepository
    {
        public Discount? Get(string code)
        {
            switch (code)
            {
                case "12345678":
                    return new Discount(10, DateTime.Now.AddDays(5));
                case "11111111":
                    return new Discount(10, DateTime.Now.AddDays(-5));
                default:
                    return null;
            }
        }
    }
}
