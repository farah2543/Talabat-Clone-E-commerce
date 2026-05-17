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
    public class ProductWithBrandAndTypeSpecification : Specifications<Product>
    {

        public ProductWithBrandAndTypeSpecification(int id) : base(prod => prod.Id == id) 
        {
            AddInclude(product => product.ProductBrand);

            AddInclude(product => product.ProductType);


        }


        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameters parameters )
            : base(/*Filter*/ product =>
                (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
                (string.IsNullOrWhiteSpace(parameters.Search) 
                || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);

            AddInclude(product => product.ProductType);

            if (parameters.Sort is not null )
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDesc(p => p.Price);
                        break;

                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;

                    case ProductSortOptions.NameDesc:
                        SetOrderByDesc(p => p.Name);
                        break;

                    default:  // This handles NameAsc and any unexpected values
                        SetOrderBy(p => p.Name);
                        break;
                }



            }

            ApplyPagination(parameters.PageIndex, parameters.PageSize);



        }





    }
}
