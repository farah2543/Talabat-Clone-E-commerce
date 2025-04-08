using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController (IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet("Products")]

        public async Task<ActionResult <IEnumerable<ProductResultDTO>>> GetAllProduct([FromQuery]ProductParametersSpecifications parameters )
        {
            var products =  await _serviceManager.ProductService.GetAllProductsAsync(parameters);   
            return Ok(products);


        }


        [HttpGet("Brands")]

        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);


        }


        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            return Ok(product);


        }



    }
}
