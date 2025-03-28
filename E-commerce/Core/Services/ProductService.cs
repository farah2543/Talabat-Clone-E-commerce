using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService (IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GenericRepository<ProductBrand, int>().GetAllAsync(true);

            var brandsResult = _mapper.Map<IEnumerable<BrandResultDTO>>(brands);

            return brandsResult;


        }

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync(string ?sort , int? brandId , int? typeId)
        {
            var product = await _unitOfWork.GenericRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecification(sort,brandId,typeId));

            var productResult = _mapper.Map<IEnumerable<ProductResultDTO>>(product);

           return productResult;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var type = await _unitOfWork.GenericRepository<ProductType, int>().GetAllAsync(true);

            var typeResult = _mapper.Map<IEnumerable<TypeResultDTO>>(type);

            return typeResult;
        }

        public async Task<ProductResultDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GenericRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecification(id));

            var productResult = _mapper.Map<ProductResultDTO>(product);

            return productResult;



        }
    }
}
