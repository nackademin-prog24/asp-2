using Grpc.Core;
using Presentation.WebApi.Data;
using Presentation.WebApi.Entitites;

namespace Presentation.WebApi.Services;

public class AccountProfileService(DataContext context) : AccountProfiler.AccountProfilerBase
{
    private readonly DataContext _context = context;

    public override async Task<AccountProfileReply> CreateAccountProfile(AccountProfileRequest request, ServerCallContext context)
    {
        try
        {
            var entity = new AccountProfileEntity
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return new AccountProfileReply
            {
                Succeeded = true,
                Message = "Account Profile was created successfully."
            };
        }
        catch (Exception ex)
        {
            return new AccountProfileReply
            {
                Succeeded = false,
                Message = ex.Message
            };
        }

    }
}
