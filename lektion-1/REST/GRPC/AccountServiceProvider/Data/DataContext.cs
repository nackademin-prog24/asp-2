using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountServiceProvider.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
}
