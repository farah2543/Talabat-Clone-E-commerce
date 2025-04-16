﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);

        Task<bool> DeleteBasketAsync(string id);

        Task<CustomerBasket?> UpdateBasket(CustomerBasket basket, TimeSpan? timeToLive = null);

    }
}
