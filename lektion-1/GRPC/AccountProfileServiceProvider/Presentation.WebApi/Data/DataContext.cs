using Microsoft.EntityFrameworkCore;
using Presentation.WebApi.Entitites;

namespace Presentation.WebApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<AccountProfileEntity> AccountProfiles { get; set; }
}
