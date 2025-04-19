using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;

using ShippingAddress = Domain.Entities.OrderEntities.Address;

namespace Services.MappingProfiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDTO>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => s.Product.PictureUrl));

            CreateMap<Order, OrderResult>()
                .ForMember(d => d.PaymentStatus, options => options.MapFrom(s => s.ToString()))
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));


        }
    }
}
