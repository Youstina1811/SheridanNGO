using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SheridanNGO.Models;
using Microsoft.AspNetCore.Identity;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DonationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity (commented out based on your current setup)
// builder.Services.AddIdentity<User, IdentityRole>()
//     .AddEntityFrameworkStores<DonationDbContext>()
//     .AddDefaultTokenProviders();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
});

// Configure the application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Add Stripe settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("AppSettings"));

// Add Stripe service to the dependency injection container
builder.Services.AddSingleton<StripeClient>(provider =>
{
    var stripeSettings = provider.GetRequiredService<IConfiguration>().GetSection("AppSettings").Get<StripeSettings>();
    return new StripeClient(stripeSettings.StripeSecretKey); // Initialize StripeClient with secret key
});

// Add MVC services
builder.Services.AddControllersWithViews();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication and Authorization middleware
app.UseAuthentication(); // Only call this once
app.UseAuthorization();  // Only call this once

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class StripeSettings
{
    public string StripeSecretKey { get; set; }
    public string StripePublishableKey { get; set; }
}
