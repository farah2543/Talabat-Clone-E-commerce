using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductCountSpecification : Specifications<Product>
    {
        public ProductCountSpecification(ProductParametersSpecifications parameters)
          : base(/*Filter*/ product =>
              (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
              (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value))
        {
          



        }

    }
}
