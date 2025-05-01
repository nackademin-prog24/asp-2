using Application.Dtos;
using Application.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public interface IVerificationService
{
    string GenerateVerificationCode();
    Task<VerificationServiceResult> SaveVerificationCodeAsync(string email, string code, int validForInMinutes = 5);
    Task<VerificationServiceResult> SendVerificationCodeAsync(string email);
    Task<VerificationServiceResult> ValidateVerificationCodeAsync(string email, string code);
}

public class VerificationService(IEmailService emailService, IMemoryCache cache) : IVerificationService
{
    private readonly IEmailService _emailService = emailService;
    private readonly IMemoryCache _cache = cache;

    public string GenerateVerificationCode()
    {
        throw new NotImplementedException();
    }

    public Task<VerificationServiceResult> SaveVerificationCodeAsync(string email, string code, int validForInMinutes = 5)
    {
        throw new NotImplementedException();
    }

    public Task<VerificationServiceResult> SendVerificationCodeAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<VerificationServiceResult> ValidateVerificationCodeAsync(string email, string code)
    {
        throw new NotImplementedException();
    }
}
