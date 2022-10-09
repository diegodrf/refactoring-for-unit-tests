using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain
{
    public class Discount: Entity
    {
        public Discount(decimal amount, DateTime expireDate)
        {
            Amount = amount;
            ExpireDate = expireDate;
        }

        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public bool IsValid() => DateTime.Compare(DateTime.Now, ExpireDate) < 0;
        public decimal Value() => IsValid() ? Amount : 0;
    }
}
