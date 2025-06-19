using ADAShop.Shared.Entities;
using ADAShop.Web.Data;
using ADAShop.Web.Repository;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using ADAShop.Web.Services.Category;
using ADAShop.Web.Services.Order;
using ADAShop.Web.Services.OrderItem;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IdentityContext>(
              options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
              });

builder.Services.AddSession();
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5238") });
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

//register the identityuser
builder.Services.AddIdentity<User, IdentityRole<long>>(
    options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.SignIn.RequireConfirmedAccount = false;
    }
    ).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();