using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetProduct()
        {
            //var typeid =1;
            //var product = _context.Products.Where( x => x.ProductTypeId ==1).Include(x =>x.ProductType).ToListAsync();
            return await _context.Products
                        .Include(p=> p.ProductType )
                        .Include(P => P.ProductBrand)
                        .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
             return await _context.ProductBrand.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                        .Include(p=> p.ProductType )
                        .Include(P => P.ProductBrand)
                         .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductType.ToListAsync();
        }
    }
}