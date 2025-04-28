using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.WebApi;
using Presentation.WebApi.Data;
using Presentation.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<AccountService>();

builder.Services.AddGrpcClient<AccountProfiler.AccountProfilerClient>(x =>
{
    x.Address = new Uri(builder.Configuration["AccountProfileServiceProvider"]!);
});

var app = builder.Build();

app.MapOpenApi();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

