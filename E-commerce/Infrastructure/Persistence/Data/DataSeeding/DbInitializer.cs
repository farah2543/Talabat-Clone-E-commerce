using Domain.Entities;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(ApplicationDbContext dbContext,RoleManager<IdentityRole> roleManager , UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                //if (_dbContext.Database.GetPendingMigrations().Any())
                //{
                    await _dbContext.Database.MigrateAsync();

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

                if (!_dbContext.DeliveryMethods.Any())

                {
                    var methodsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodsData);

                    if (methods is not null && methods.Any())
                    {
                        await _dbContext.DeliveryMethods.AddRangeAsync(methods);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                // }

            }
            catch (Exception e)
            {
            }



        }

        public async Task InitializeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01013145678"
                };

                var superAdminUser = new User()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01013145678"
                };

                await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                await _userManager.CreateAsync(superAdminUser, "P@ssw0rd0");


                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
             
            }
        }
    }
}
