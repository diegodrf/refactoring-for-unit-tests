using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class OrderItem: Entity
    {
        public OrderItem(Product product, int quantity)
        {
            var keyPrefix = nameof(OrderItem) + '.';
            AddNotifications(new Contract<OrderItem>()
                .Requires()
                .IsNotNull(product, keyPrefix + nameof(Product), "Invalid product")
                .IsGreaterThan(quantity, 0, keyPrefix + nameof(Quantity), "The product quantity must be greater than zero")
                );


            Product = product;
            Price = 0;
            Quantity = quantity;
        }

        public Product Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}
