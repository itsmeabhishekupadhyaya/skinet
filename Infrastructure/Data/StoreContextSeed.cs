using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async  Task SeedAsync(StoreContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrand.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands =JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var item in brands)
                    {
                        context.ProductBrand.Add(item);
                        
                    }
                    await context.SaveChangesAsync();
                }

                if(!context.ProductType.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var type =JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    foreach (var item in type)
                    {
                        context.ProductType.Add(item);
                        
                    }
                    await context.SaveChangesAsync();
                }

                 if(!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var product =JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in product)
                    {
                        context.Products.Add(item);
                        
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError (ex.Message);
            }
        }
    }
}