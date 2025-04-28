using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Models;
using Presentation.WebApi.Services;

namespace Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AccountService accountService) : ControllerBase
{
    private readonly AccountService _accountService = accountService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.CreateUserAsync(formData);
        return result ? Ok() : Problem();
    }
}
