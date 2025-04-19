using Shared.OrderModels;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIDAsync(Guid id);

        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);



        Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);


        Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync();








    }
}