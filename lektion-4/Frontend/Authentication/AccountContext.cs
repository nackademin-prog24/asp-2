using Authentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication;

public class AccountContext(DbContextOptions<AccountContext> options) : IdentityDbContext<AccountUser>(options)
{
}
