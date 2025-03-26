using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync();

        Task<ProductResultDTO?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();

    }
}

