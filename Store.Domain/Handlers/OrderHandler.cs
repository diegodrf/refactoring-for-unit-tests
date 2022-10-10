using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandler(
            IOrderRepository orderRepository, 
            ICustomerRepository customerRepository, 
            IDeliveryFeeRepository deliveryFeeRepository, 
            IDiscountRepository discountRepository,
            IProductRepository productRepository
            )
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;

        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            /// Fail Fast validation
            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Invalid Order", command.Notifications);
            }

            /// 1. Get cliente from Database
            var customer = _customerRepository.Get(command.CustomerDocumentNumber);

            /// 2. Calculate delivery fee
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            /// 3. Get Discount voucher from database
            var discount = command?.PromoCode != null 
                ? _discountRepository.Get(command.PromoCode)
                : null;

            /// 4. Generate order
            var products = _productRepository.Get(command!.Items.Select(_ => _.ProductId));
            var order = new Order(customer, deliveryFee, discount);
            foreach(var item in command.Items)
            {
                var product = products.Where(_ => _.Id == item.ProductId).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            /// 5. Group notifications
            AddNotifications(order.Notifications);

            /// 6. Verify notifications
            if (!IsValid)
            {
                return new GenericCommandResult(false, "Error during order generation", Notifications);
            }

            /// 7. Return result
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Order {order.Number} generated with Success", order);
        }
    }
}
