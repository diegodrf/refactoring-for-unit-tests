using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        
        private const string _customerDocumentNumber = "12345678901";
        private const string _zipCode = "11111111111";
        private const string _promoCode = "12345678";

        public OrderHandlerTests()
        {
            _orderRepository = new OrderRepositoryMock();
            _customerRepository = new CustomerRepositoryMock();
            _deliveryFeeRepository = new DeliveryFeeRepositoryMock();
            _discountRepository = new DiscountRepositoryMock();
            _productRepository = new ProductRepositoryMock();

        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var customerDocumentNumber = "11111111111";
            var command = new CreateOrderCommand(customerDocumentNumber, _zipCode, _promoCode);

            while (command.Items.Count < 10)
            {
                command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            }

            var orderHandler = new OrderHandler(
                _orderRepository,
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsFalse(orderHandler.IsValid);
            Assert.IsFalse(result!.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var zipCode = "11";
            
            var command = new CreateOrderCommand(_customerDocumentNumber, zipCode, _promoCode);
            command.Validate();

            while (command.Items.Count < 1)
            {
                command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            }

            var orderHandler = new OrderHandler(
                _orderRepository,
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsFalse(result!.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var promoCode = "11111111";
            var command = new CreateOrderCommand(_customerDocumentNumber, _zipCode, promoCode);
            command.Validate();

            while (command.Items.Count < 1)
            {
                command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            }

            var orderHandler = new OrderHandler(
                _orderRepository,
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsFalse(result!.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand(_customerDocumentNumber, _zipCode, _promoCode);
            command.Validate();

            var orderHandler = new OrderHandler(
                _orderRepository,
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsFalse(result!.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            string customerDocumentNumber = "0";
            var command = new CreateOrderCommand(customerDocumentNumber, _zipCode, _promoCode);
            command.Validate();

            while (command.Items.Count < 1)
            {
                command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 0));
            }

            var orderHandler = new OrderHandler(
                _orderRepository,
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsFalse(result!.Success);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand(_customerDocumentNumber, _zipCode, _promoCode);
            
            while(command.Items.Count < 10)
            {
                command.AddOrderItemCommand(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            }
            
            var orderHandler = new OrderHandler(
                _orderRepository, 
                _customerRepository, 
                _deliveryFeeRepository, 
                _discountRepository, 
                _productRepository
            );

            var result = orderHandler.Handle(command) as GenericCommandResult;

            Assert.IsTrue(orderHandler.IsValid);
            Assert.IsTrue(result!.Success);
        }
    }
}
