using Microsoft.AspNetCore.Identity;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Collaborator", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var adminUser = new ApplicationUser { UserName = "admin@onlineshop.com", Email = "admin@onlineshop.com", FirstName = "Admin", LastName = "Shop" };
            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var collaboratorUser = new ApplicationUser { UserName = "colaborator@onlineshop.com", Email = "colaborator@onlineshop.com", FirstName = "Collab", LastName = "Orator" };
            if (await userManager.FindByEmailAsync(collaboratorUser.Email) == null)
            {
                await userManager.CreateAsync(collaboratorUser, "Collab123!");
                await userManager.AddToRoleAsync(collaboratorUser, "Collaborator");
            }

            var normalUser = new ApplicationUser { UserName = "user@onlineshop.com", Email = "user@onlineshop.com", FirstName = "Ion", LastName = "Popescu" };
            if (await userManager.FindByEmailAsync(normalUser.Email) == null)
            {
                await userManager.CreateAsync(normalUser, "User123!");
                await userManager.AddToRoleAsync(normalUser, "User");
            }

            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Electronice", Description = "Dispozitive electronice diverse" },
                    new Category { Name = "Carti", Description = "Carti si publicatii" },
                    new Category { Name = "Fashion", Description = "Imbracaminte si accesorii" },
                    new Category { Name = "Home & Garden", Description = "Articole pentru casa si gradina" },
                    new Category { Name = "Sport", Description = "Echipamente de sport" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var collaborator = await userManager.FindByEmailAsync("colaborator@onlineshop.com");

                var products = new[]
                {
                    new Product { Title = "Laptop Gaming ASUS ROG", Description = "NVIDIA RTX 4090, Intel i9-13900K, 32GB RAM, 1TB SSD", Price = 5999.99m, Stock = 12, ImagePath = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "MacBook Pro 16 M3 Max", Description = "Apple Silicon M3 Max, 36GB Memory, 1TB SSD", Price = 3999.99m, Stock = 8, ImagePath = "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Monitor Gaming 4K 144Hz", Description = "ASUS ROG Swift, HDMI 2.1, HDR 1000", Price = 1299.99m, Stock = 20, ImagePath = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Tastatura Mecanica RGB", Description = "Cherry MX Brown, Wireless, Aluminum Case", Price = 349.99m, Stock = 50, ImagePath = "https://images.unsplash.com/photo-1587829191301-72f41d3228f0?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Mouse Gaming Logitech", Description = "PMW3389 Sensor, 16000 DPI, Wireless", Price = 199.99m, Stock = 65, ImagePath = "https://images.unsplash.com/photo-1527814050087-3793815479db?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Casti Gaming HyperX", Description = "Surround 7.1, USB 2.0, Noise Cancelling", Price = 449.99m, Stock = 30, ImagePath = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "SSD Samsung 970 EVO 2TB", Description = "NVMe M.2 PCIe 4.0, 7400MB/s Read", Price = 299.99m, Stock = 40, ImagePath = "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Router WiFi 6E ASUS", Description = "Tri-band WiFi 6E, Mesh Ready, Gaming Optimized", Price = 599.99m, Stock = 15, ImagePath = "https://images.unsplash.com/photo-1598593867769-6d5fb1e6d27d?w=500&h=500&fit=crop", CategoryId = 1, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    
                    new Product { Title = "C# Advanced - Erik Meijer", Description = "1200 pagini, async/await, LINQ, Reflection", Price = 89.99m, Stock = 25, ImagePath = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=500&h=500&fit=crop", CategoryId = 2, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = ".NET 8 In Action", Description = "800 pagini, API REST, Microservices, Cloud", Price = 79.99m, Stock = 30, ImagePath = "https://images.unsplash.com/photo-1507842217343-583f20270319?w=500&h=500&fit=crop", CategoryId = 2, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Clean Code - Robert Martin", Description = "464 pagini, Best practices, Design patterns", Price = 69.99m, Stock = 45, ImagePath = "https://images.unsplash.com/photo-1543002588-d0a9b7c98f0f?w=500&h=500&fit=crop", CategoryId = 2, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Design Patterns Go Lang", Description = "600 pagini, Creational, Structural Patterns", Price = 84.99m, Stock = 20, ImagePath = "https://images.unsplash.com/photo-1506880018603-83d5b814b5a6?w=500&h=500&fit=crop", CategoryId = 2, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Microservices Patterns", Description = "700 pagini, CQRS, Event Sourcing, Saga", Price = 94.99m, Stock = 18, ImagePath = "https://images.unsplash.com/photo-1535905557558-afc4877a26fc?w=500&h=500&fit=crop", CategoryId = 2, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    
                    new Product { Title = "Tricou Premium Cotton", Description = "100% bumbac organic, greu 200g", Price = 79.99m, Stock = 120, ImagePath = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=500&h=500&fit=crop", CategoryId = 3, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Hoodie Negru Unisex", Description = "Fleece 300g, cuvete laterale, buzunare", Price = 149.99m, Stock = 80, ImagePath = "https://images.unsplash.com/photo-1556821552-5f6c4d334635?w=500&h=500&fit=crop", CategoryId = 3, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Jeans Slim Fit Denim", Description = "99% bumbac, 1% elastan, albastru inchis", Price = 199.99m, Stock = 60, ImagePath = "https://images.unsplash.com/photo-1542272604-787c62d465d1?w=500&h=500&fit=crop", CategoryId = 3, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Sapca Baseball Dad Hat", Description = "100% bumbac, snapback ajustabil", Price = 49.99m, Stock = 150, ImagePath = "https://images.unsplash.com/photo-1572365992253-3cb3e56dd362?w=500&h=500&fit=crop", CategoryId = 3, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    
                    new Product { Title = "Plant Pot Ceramic 30cm", Description = "Ceramica glazurata, drenaj integrat", Price = 89.99m, Stock = 100, ImagePath = "https://images.unsplash.com/photo-1578482595012-89bed96c5245?w=500&h=500&fit=crop", CategoryId = 4, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Lampa LED Inteligenta RGB", Description = "16M culori, WiFi, Alexa Compatible", Price = 129.99m, Stock = 45, ImagePath = "https://images.unsplash.com/photo-1565636192335-14c46fa1120d?w=500&h=500&fit=crop", CategoryId = 4, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Perna Ergonomica Memory Foam", Description = "Ortopedica, Gel cooling, hipoalergen", Price = 159.99m, Stock = 70, ImagePath = "https://images.unsplash.com/photo-1584622180454-c8b9d81bd562?w=500&h=500&fit=crop", CategoryId = 4, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Patura Electrica 220V", Description = "2 Zone temperatura, Auto shutoff 8 ore", Price = 199.99m, Stock = 35, ImagePath = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=500&h=500&fit=crop", CategoryId = 4, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    
                    new Product { Title = "Dumbbell Set Hexagon 20kg", Description = "2x10kg, Placi fier, gripper cauciuc", Price = 299.99m, Stock = 25, ImagePath = "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=500&h=500&fit=crop", CategoryId = 5, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Yoga Mat TPE NonSlip", Description = "6mm Thickness, Eco-friendly", Price = 79.99m, Stock = 90, ImagePath = "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop", CategoryId = 5, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Banda Elastica Fitness 5buc", Description = "5 Nivele de rezistenta, Set complet", Price = 49.99m, Stock = 120, ImagePath = "https://images.unsplash.com/photo-1517836357463-d25ddfcbf042?w=500&h=500&fit=crop", CategoryId = 5, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now },
                    new Product { Title = "Aparat Exercitiu AB Roller", Description = "Dual wheels, Knee cushion, Antiderapant", Price = 129.99m, Stock = 50, ImagePath = "https://images.unsplash.com/photo-1599058917212-d217cde485a0?w=500&h=500&fit=crop", CategoryId = 5, CreatedByUserId = collaborator?.Id, Status = ProductStatus.Aprobat, CreatedAt = DateTime.Now }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
