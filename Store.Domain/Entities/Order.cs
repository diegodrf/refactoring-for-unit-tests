using Flunt.Validations;
using Store.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Order: Entity
    {
        private readonly IList<OrderItem> _items;
        public Order(Customer customer, decimal deliveryFee, Discount? discount = null)
        {
            AddNotifications(new Contract<Order>()
                .Requires()
                .IsNotNull(customer, $"{nameof(Order)}.{nameof(Customer)}", "Invalid client")
                );

            Customer = customer;
            Date = DateTime.Now.Date;
            Number = Guid.NewGuid().ToString("N")[..8];
            DeliveryFee = deliveryFee;
            Discount = discount;
            Status = EOrderStatus.WaitingPayment;
            _items = new List<OrderItem>();
        }

        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public Discount? Discount { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items
        {
            get 
            { 
                return _items.ToArray(); 
            }
        }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.IsValid)
            {
                _items.Add(item);
            }
               
        }

        public decimal Total()
        {
            var total = _items.Sum(x => x.Total());
            total += DeliveryFee;
            total -= Discount?.Value() ?? 0;
            return total;
        }

        public void Pay(decimal amount)
        {
            if(amount == Total())
            {
                Status = EOrderStatus.WaitingDelivery;
            }
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}
