using AccountServiceProvider.Models;
using Microsoft.AspNetCore.Identity;

namespace AccountServiceProvider.Services;

public class AccountService(UserManager<IdentityUser> userManager, IConfiguration configuration)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<bool> CreateUserAsync(RegistrationFormData formData)
    {
        var user = new IdentityUser
        {
            UserName = formData.Email,
            Email = formData.Email,
        };

        var result = await _userManager.CreateAsync(user, formData.Password);
        if (result.Succeeded)
        {

        }

        return result.Succeeded;
    }
}
