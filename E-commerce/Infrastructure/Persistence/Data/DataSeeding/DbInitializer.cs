using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\types.json");
                    // Convert from json to c# object [deserialization]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _dbContext.AddRangeAsync(types);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\brands.json");
                    // Convert from json to c# object [deserialization]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null && brands.Any())
                    {
                        await _dbContext.AddRangeAsync(brands);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\products.json");
                    // Convert from json to c# object [deserialization]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.AddRangeAsync(products);
                        await _dbContext.SaveChangesAsync();
                    }
                }



            }
            catch (Exception e)
            {
            }



        }
    }
}
