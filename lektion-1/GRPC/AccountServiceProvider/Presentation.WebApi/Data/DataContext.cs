using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
}
