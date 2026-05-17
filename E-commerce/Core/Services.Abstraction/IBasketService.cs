using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO?> GetBasketAsync (string id);
         
        Task <bool> DeleteBasketAsync (string id);

        Task<BasketDTO?> UpdateBasketAsync (BasketDTO basket);


    }
}
