using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    public class CustomerRepositoryMock : ICustomerRepository
    {
        public Customer? Get(string documentNumber)
        {
            if(documentNumber == "12345678901")
            {
                return new Customer("Bruce Wayne", "batman@balta.io");
            }
            return null;
        }
    }
}
