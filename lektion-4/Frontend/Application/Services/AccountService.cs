using Application.Dtos;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public interface IAccountService
{
    Task<AccountServiceResult> FindByEmailAsync(string email);
    Task<AccountServiceResult<string>> CreateAccountAsync(AccountUser user, string password);
}


public class AccountService(UserManager<AccountUser> userManager) : IAccountService
{
    private readonly UserManager<AccountUser> _userManager = userManager;

    public Task<AccountServiceResult<string>> CreateAccountAsync(AccountUser user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AccountServiceResult> FindByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
