using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandsSpecification()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x =>x.ProductType);
        }

        public ProductWithTypeAndBrandsSpecification(int id) : base(x => x.Id==id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x =>x.ProductType);
        }
    }
}