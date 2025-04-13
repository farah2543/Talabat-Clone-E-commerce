using AutoMapper;
using Domain.Entities;
using Shared.DTOs;

namespace Services.MappingProfiles
{
    public class BasketProfile :Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
