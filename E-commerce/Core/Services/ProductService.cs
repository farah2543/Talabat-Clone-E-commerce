using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class ProductService (IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GenericRepository<ProductBrand, int>().GetAllAsync();

            var brandsResult = _mapper.Map<IEnumerable<BrandResultDTO>>(brands);

            return brandsResult;


        }

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync()
        {
            var product = await _unitOfWork.GenericRepository<Product, int>().GetAllAsync();

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
            var product = await _unitOfWork.GenericRepository<Product, int>().GetByIdAsync(id);

            var productResult = _mapper.Map<ProductResultDTO>(product);

            return productResult;



        }
    }
}
