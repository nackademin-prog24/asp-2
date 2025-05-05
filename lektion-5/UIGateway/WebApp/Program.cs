using Authentication.Contexts;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<AuthService>();
builder.Services.AddDbContext<AuthenticationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AccountDatabase")));
builder.Services.AddIdentity<User, IdentityRole>(x => 
{ 
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/auth/login";
    x.Cookie.SameSite = SameSiteMode.None;
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromDays(14);
    x.Cookie.IsEssential = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(x =>
{
    x.ClientId = builder.Configuration["Google:ClientId"]!;
    x.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
});

var app = builder.Build();
app.UseHsts();
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
