using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _productbrandrepo;
        private readonly IGenericRepository<ProductType> _producttyperepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productrepo, IGenericRepository<ProductBrand> productbrandrepo,
                                     IGenericRepository<ProductType> producttyperepo, IMapper mapper)
        {
            _mapper = mapper;
            _producttyperepo = producttyperepo;
            _productbrandrepo = productbrandrepo;
            _productrepo = productrepo;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandsSpecification();

            var product = await _productrepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(product).ToList());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var spec = new ProductWithTypeAndBrandsSpecification(id);


            var product = await _productrepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product,ProductToReturnDto>(product);

         
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _productbrandrepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {

            return Ok(await _producttyperepo.ListAllAsync());
        }
    }
}