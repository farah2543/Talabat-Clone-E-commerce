using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters);

        Task<ProductResultDTO> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();

    }
}

