using AutoMapper;
using Domain.Entities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandResultDTO>();

            CreateMap<Product, ProductResultDTO>();

            // Map From Product to ProductResultDto
            CreateMap<Product, ProductResultDTO>()
                .ForMember(D => D.BrandName, options => options.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.TypeName, options => options.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.PictureUrl, options => options.MapFrom<PictureURLResolver>());




        }

    }
}
