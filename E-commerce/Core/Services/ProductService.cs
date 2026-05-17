using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions.NotFoundExceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs;

namespace Services
{
    public class ProductService (IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GenericRepository<ProductBrand, int>().GetAllAsync();

            var brandsResult = _mapper.Map<IEnumerable<BrandResultDTO>>(brands);

            return brandsResult;


        }

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var product = await _unitOfWork.GenericRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecification(parameters));

            var totalCount = await _unitOfWork.GenericRepository<Product, int>().CountAsync(new ProductCountSpecification(parameters));


            var productResult = _mapper.Map<IEnumerable<ProductResultDTO>>(product);

            //return productResult;

            var result = new PaginatedResult<ProductResultDTO>(
                productResult.Count(),
                parameters.PageIndex,
                totalCount,
                productResult
                );
            return result;

        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var type = await _unitOfWork.GenericRepository<ProductType, int>().GetAllAsync();

            var typeResult = _mapper.Map<IEnumerable<TypeResultDTO>>(type);

            return typeResult;
        }

        public async Task<ProductResultDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GenericRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecification(id));

            //var productResult = _mapper.Map<ProductResultDTO>(product);

            //return productResult;

            return product is null  ? throw new ProductNotFoundException(id) : _mapper.Map<ProductResultDTO>(product);



        }
    }
}
