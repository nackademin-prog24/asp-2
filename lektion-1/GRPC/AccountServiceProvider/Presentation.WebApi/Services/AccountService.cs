using Microsoft.AspNetCore.Identity;
using Presentation.WebApi.Models;
namespace Presentation.WebApi.Services;

public class AccountService(UserManager<IdentityUser> userManager, AccountProfiler.AccountProfilerClient accountProfilerClient)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly AccountProfiler.AccountProfilerClient _accountProfilerClient = accountProfilerClient;


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
            var request = new AccountProfileRequest
            {
                UserId = user.Id,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                PhoneNumber = formData.PhoneNumber,
            };

            var response = await _accountProfilerClient.CreateAccountProfileAsync(request);
        }

        return result.Succeeded;
    }
}
