using ADAShop.Api.Helpers;
using ADAShop.Shared.Emuns;
using ADAShop.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Data
{
    public class SeedDb
    {
        private readonly Context _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(Context context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckCategoriesAsync();
            await CheckProductsAsync();
            await CheckUserAsync("ADMIN", "ADA", "ada", "admin_ada@yopmail.com", "+57 301 101 1150", "1734 Stonecoal Road Medellín", UserTypeEnum.Admin);
            await CheckUserAsync("Joann", "Quintero", "joannq", "admin_joann@yopmail.com", "+57 301 101 1150", "1734 Stonecoal Road Bogotá", UserTypeEnum.Admin);
            await CheckUserAsync("John", "Smit", "johns", "user_ada@yopmail.com", "+57 301 101 1150", "1734 Stonecoal Road Medellín", UserTypeEnum.User);
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserTypeEnum.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserTypeEnum.User.ToString());
        }

        private async Task<User> CheckUserAsync(string name, string lastName, string userName, string email, string phone, string address, UserTypeEnum userType)
        {
            var user = await _userHelper.GetUserAsync(userName);
            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    LastName = lastName,
                    Email = email,
                    UserName = userName,
                    PhoneNumber = phone,
                    Address = address
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Smartphones" });
                _context.Categories.Add(new Category { Name = "Laptops" });
                _context.Categories.Add(new Category { Name = "Cámaras" });
                _context.Categories.Add(new Category { Name = "Accesorios" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                var CategorySmartphones = await _context.Categories.FirstOrDefaultAsync(x => x.Id == 1);
                var CategoryLaptops = await _context.Categories.FirstOrDefaultAsync(x => x.Id == 2);

                _context.Products.Add(new Product { Name = "Portatil ACER", Price = 75000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 10 });
                _context.Products.Add(new Product { Name = "Mouse Inalámbrico", Price = 125000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 20, Category = CategoryLaptops, Image = "bd45a431-99ec-4a7d-af28-7b6fe7f0e0f6_ausus.png" });
                _context.Products.Add(new Product { Name = "Iphone 16", Price = 125000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 40, Category = CategorySmartphones, Image = "" });
                _context.Products.Add(new Product { Name = "Samsung S22N", Price = 250000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 21, Image = "bd45a431-99ec-4a7d-af28-7b6fe7f0e0f6_kodb.png" });
                _context.Products.Add(new Product { Name = "Samsung S22", Price = 250000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 21 });
                _context.Products.Add(new Product { Name = "Samsung S22E", Price = 89211, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 51 });
                _context.Products.Add(new Product { Name = "Iphone 14", Price = 5211, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 20, Image = "8297402e-7e4f-4a9f-9789-6581d746a6f1_a13.png" });
                await _context.SaveChangesAsync();
            }
        }
    }
}