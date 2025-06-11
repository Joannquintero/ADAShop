using ADAShop.Web.Repository;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using ADAShop.Web.Services.Category;
using ADAShop.Web.Services.Order;
using ADAShop.Web.Services.OrderItem;
using ADAShop.Web.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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

app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();