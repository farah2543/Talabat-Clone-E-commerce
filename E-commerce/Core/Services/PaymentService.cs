using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Entities;
using Domain.Exceptions.NotFoundExceptions;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;
using Services.Abstraction;
using AutoMapper;
using Stripe;
using Product = Domain.Entities.Product;

namespace Services
{
    public class PaymentService (IBasketRepository basketRepository 
        , IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];
            var basket = await basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            foreach (var item in basket.items)
            {
                var product = await unitOfWork.GenericRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No delivery method was selected");
            var deliveryMethod = await unitOfWork.GenericRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;

            // Long == dollar ==> cent
            var amount = (long)(basket.items.Sum(i => i.Price * i.Quantity) + basket.ShippingPrice) * 100;
            var service = new PaymentIntentService();

            // If he want to create or update
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await service.CreateAsync(createOptions); // Id , ClientSecret
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            await basketRepository.UpdateBasket(basket);

            return mapper.Map<BasketDTO>(basket);

        }
    }
}
