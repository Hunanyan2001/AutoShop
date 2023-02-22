using AutoShop.Models;
using AutoShop;
using AutoShop.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

IConfigurationBuilder _confString = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
IConfigurationRoot root = _confString.Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();    // подключение аутентификации
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.MapRazorPages();
app.UseDeveloperExceptionPage();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.Run();
