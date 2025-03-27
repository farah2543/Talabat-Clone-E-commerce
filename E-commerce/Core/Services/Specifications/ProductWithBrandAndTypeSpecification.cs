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


        public ProductWithBrandAndTypeSpecification(string ? sort, int? brandId , int? typeId)
            : base(/*Filter*/ product =>
                (!brandId.HasValue || product.BrandId == brandId.Value) &&
                (!typeId.HasValue || product.TypeId == typeId.Value)
)
        {
            AddInclude(product => product.ProductBrand);

            AddInclude(product => product.ProductType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDesc(p => p.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        SetOrderByDesc(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }



            }



        }

    }
}
