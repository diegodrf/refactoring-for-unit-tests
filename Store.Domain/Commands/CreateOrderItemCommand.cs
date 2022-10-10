using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable<Notification>, Icommand
    { 
        public CreateOrderItemCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateOrderItemCommand>()
                .Requires()
                .AreEquals(ProductId.ToString().Length, 32, "Product", "Invalid product")
                .IsGreaterThan(Quantity, 0, "Quantity", "Invalid quantity")
            );
        }
    }
}
