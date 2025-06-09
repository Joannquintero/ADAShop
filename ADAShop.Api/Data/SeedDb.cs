using ADAShop.Shared.Entities;

namespace ADAShop.Api.Data
{
    public class SeedDb
    {
        private readonly Context _context;

        public SeedDb(Context context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            //await CheckUserAdminAsync();
            await CheckCategoriesAsync();
            await CheckProductsAsync();
        }

        private async Task CheckRolesAsync()
        {
            //await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            //await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        #region MyRegion

        //private async Task<User> CheckUserAdminAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        //{
        //var user = await _userHelper.GetUserAsync(email);
        //if (user == null)
        //{
        //    var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
        //    if (city == null)
        //    {
        //        city = await _context.Cities.FirstOrDefaultAsync();
        //    }

        //    string filePath;
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //    {
        //        filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
        //    }
        //    else
        //    {
        //        filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
        //    }

        //    var fileBytes = File.ReadAllBytes(filePath);
        //    var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

        //    user = new User
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Email = email,
        //        UserName = email,
        //        PhoneNumber = phone,
        //        Address = address,
        //        Document = document,
        //        City = city,
        //        UserType = userType,
        //        Photo = imagePath,
        //    };

        //    await _userHelper.AddUserAsync(user, "123456");
        //    await _userHelper.AddUserToRoleAsync(user, userType.ToString());

        //    var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //    await _userHelper.ConfirmEmailAsync(user, token);
        //}

        //return user;
        //}

        #endregion MyRegion

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
                _context.Products.Add(new Product { Name = "Portatil ACER", Price = 75000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 10 });
                _context.Products.Add(new Product { Name = "Mouse Inalámbrico", Price = 125000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 20 });
                _context.Products.Add(new Product { Name = "Iphone 16", Price = 125000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 40 });
                _context.Products.Add(new Product { Name = "Samsung S22", Price = 250000, Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500.", Stock = 21 });
                await _context.SaveChangesAsync();
            }
        }
    }
}