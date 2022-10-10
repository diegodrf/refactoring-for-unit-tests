using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_commando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var customerName = string.Empty;
            var command = new CreateOrderCommand(customerName, "11111111");
            command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.IsFalse(command.IsValid);
        }
    }
}
