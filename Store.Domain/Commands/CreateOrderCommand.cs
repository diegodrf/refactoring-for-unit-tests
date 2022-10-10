using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, Icommand
    {
        private readonly ICollection<CreateOrderItemCommand> _items;

        public CreateOrderCommand(string customerName, string zipCode, string? promoCode = null)
        {
            CustomerName = customerName;
            ZipCode = zipCode;
            PromoCode = promoCode;
            _items = new List<CreateOrderItemCommand>();
        }

        public string CustomerName { get; private set; }
        public string ZipCode { get; private set; }
        public string? PromoCode { get; private set; }
        public IReadOnlyCollection<CreateOrderItemCommand> Items { get { return _items.ToArray(); } }

        public void AddOrderItemCommand(CreateOrderItemCommand orderItemCommand)
        {
            _items.Add(orderItemCommand);
        }

        public void Validate()
        {
            AddNotifications(new Contract<CreateOrderCommand>()
                .Requires()
                .AreEquals(CustomerName.Length, 11, "Customer", "Invalid customer")
                .AreEquals(ZipCode.Length, 8, "ZipCode", "Invalid zipcode")
                );

            _items.ToList()
                .ForEach(
                    _ => AddNotifications(_.Notifications)
                );


        }
    }
}
