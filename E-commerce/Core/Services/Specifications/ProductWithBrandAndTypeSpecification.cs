using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification : Specifications<Product>
    {

        public ProductWithBrandAndTypeSpecification(int id) : base(prod => prod.Id == id) 
        {
            AddInclude(product => product.ProductBrand);

            AddInclude(product => product.ProductType);


        }


        public ProductWithBrandAndTypeSpecification() : base(null)
        {
            AddInclude(product => product.ProductBrand);

            AddInclude(product => product.ProductType);


        }

    }
}
