using Application.Models;
using Application.Services;
using Authentication;
using Authentication.Entities;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["AzureCommunicationSettings:ConnectionString"]));
builder.Services.Configure<AzureCommunicationSettings>(builder.Configuration.GetSection("AzureCommunicationSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountProfileService, AccountProfileService>();

builder.Services.AddDbContext<AccountContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AccountDatabaseConnection")));
builder.Services.AddDbContext<AccountProfileContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AccountProfileDatabaseConnection")));
builder.Services.AddIdentity<AccountUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = true;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/login";
    x.AccessDeniedPath = "/accessdenied";
    x.Cookie.IsEssential = true;
    x.ExpireTimeSpan = TimeSpan.FromDays(30);
    x.SlidingExpiration = true;
    x.Cookie.SameSite = SameSiteMode.Lax;
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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
