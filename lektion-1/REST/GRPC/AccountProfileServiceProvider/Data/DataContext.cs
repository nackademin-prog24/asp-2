using AccountProfileServiceProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountProfileServiceProvider.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<AccountProfileEntity> AccountProfiles { get; set; }
}
