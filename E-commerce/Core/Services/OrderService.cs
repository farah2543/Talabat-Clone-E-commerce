using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.OrderModels;
using ShippingAddress = Domain.Entities.OrderEntities.Address;


namespace Services
{
    internal class OrderService (IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var shippingAddress = mapper.Map<ShippingAddress>(request.ShippingAddress);

            var basket = await basketRepository.GetBasketAsync(request.BasketId)
                ?? throw new BasketNotFoundException(request.BasketId);

            var orderItems = new List<OrderItem>(); 

            foreach (var item in basket.items)
            {
                var product = await unitOfWork.GenericRepository    <Product, int>()
                    .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item,product));
            }

            var deliveryMethod = await unitOfWork.GenericRepository<DeliveryMethod, int>()
                .GetByIdAsync(request.DeliveryMethodId)?? throw new DeliveryMethodNotFoundException(request.DeliveryMethodId) ;

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            
            var order = new Order(userEmail,shippingAddress,orderItems,deliveryMethod,subtotal);

            await unitOfWork.GenericRepository<Order,Guid >().AddAsync(order);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderResult>(order);



        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl)
                ,item.Quantity,product.Price);
     
        public Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIDAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
