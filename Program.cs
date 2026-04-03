using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VietMachWeb.Configurations;
using VietMachWeb.Data;
using VietMachWeb.Services.Implementations.Admin;
using VietMachWeb.Services.Interfaces.Admin;

var builder = WebApplication.CreateBuilder(args);

// =======================
// 1. MVC
// =======================
builder.Services.AddControllersWithViews();

// =======================
// 2. Database & Email
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("Email"));

// =======================
// 3. HttpContextAccessor
// =======================
builder.Services.AddHttpContextAccessor();

// =======================
// 4. Authentication (COOKIE)
// =======================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login";
        options.AccessDeniedPath = "/admin/login";

        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// =======================
// 5. Dependency Injection for Services
// =======================
builder.Services.AddScoped<IAccountService, AccountService>();



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
