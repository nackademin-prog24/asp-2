using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;


    public async Task<AuthResult> SignUpExternalAsync(string email, ExternalLoginInfo info)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("User with same email already exists");

            var user = new User
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result.Succeeded
                ? new AuthResult { Succeeded = true }
                : new AuthResult { Message = string.Join(", ", result.Errors) };
        }
        catch (Exception ex)
        {
            return new AuthResult { Message = ex.Message };
        }
    }

    public async Task<AuthResult> SignUpAsync(string email, string password)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("User with same email already exists");

            var user = new User
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded
                ? new AuthResult {  Succeeded = true } 
                : new AuthResult { Message = string.Join(", ", result.Errors) }; 
        }
        catch (Exception ex)
        {
            return new AuthResult { Message = ex.Message };
        }
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded
            ? new AuthResult { Succeeded = true }
            : new AuthResult { Message = "Invalid email or password." };
    }

    public async Task<AuthResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Succeeded = true };
    }
}
