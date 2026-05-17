using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    public class OrdersController : ApiController
    {
        private readonly IServiceManager _serviceManager;

        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetAllOrdersByEmailAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrder(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIDAsync(id);
            return Ok(order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            return Ok(await _serviceManager.OrderService.GetAllDeliveryMethodsAsync());
        }
    }
}
