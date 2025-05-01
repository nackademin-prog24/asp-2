using Application.Dtos;
using Application.Models;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;

namespace Application.Services;

public interface IEmailService
{
    Task<EmailServiceResult> SendEmailAsync(EmailMessageModel message);
}

public class EmailService(EmailClient client, IOptions<AzureCommunicationSettings> options) : IEmailService
{
    private readonly EmailClient _client = client;
    private readonly AzureCommunicationSettings _settings = options.Value;

    public Task<EmailServiceResult> SendEmailAsync(EmailMessageModel message)
    {
        throw new NotImplementedException();
    }
}
