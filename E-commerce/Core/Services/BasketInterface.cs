using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketInterface (IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public Task<bool> DeleteBasketAsync(string id)
            => _basketRepository.DeleteBasketAsync(id);
       
        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return basket is null? throw new BasketNotFoundException(id)  : _mapper.Map<BasketDTO?>(basket);
           
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = await _basketRepository.UpdateBasket (_mapper.Map<CustomerBasket>(basket));

            return customerBasket is null ? throw new Exception("Cannot update this basket right now ") : _mapper.Map<BasketDTO?>(customerBasket);

        }
    }
}
