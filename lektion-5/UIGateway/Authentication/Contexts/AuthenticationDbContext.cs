using Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Contexts;

public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : IdentityDbContext<User>(options)
{
}
