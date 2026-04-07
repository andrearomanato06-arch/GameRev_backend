using FluentValidation;
using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Responses;
using GameRev.Services.Auth.Interfaces;
using GameRev.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IAccessService accessService;

    private readonly IValidator<RegistrationRequest> registrationValidator;
    private readonly IValidator<LoginRequest> loginValidator;

    public AuthController(IAccessService accessService, IUserSessionService userSessionService)
    {
        this.accessService = accessService;
    }

    [HttpPost("user/register")]
    public async Task<IActionResult> RegisterUser ([FromBody] RegistrationRequest request, CancellationToken ct)
    {

        var userRequest = RequestToRequest.RegistrationRequestToUserRequest(request);
        var user = await accessService.Register(userRequest, ct);
        if(user is null)
        {
            return BadRequest("An error occurred during the registration process, try again later");
        }
        return Ok("Registration completed successfully!");
    }

    [HttpPost("admin/register")]
    public async Task<IActionResult> RegisterAdmin ([FromBody] RegistrationRequest request, CancellationToken ct)
    {
        var validationResut = await registrationValidator.ValidateAsync(request,ct);
        if (!validationResut.IsValid)
        {
            return BadRequest("Invalid data given during registration");
        }
        var userRequest = RequestToRequest.RegistrationRequestToAdminRequest(request);
        var user = await accessService.Register(userRequest, ct);
        if(user is null)
        {
            return BadRequest("An error occurred during the registration process, try again later");
        }
        return Ok("Registration completed successfully!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login ([FromBody] LoginRequest request, CancellationToken ct)
    {
        var validationResut = await loginValidator.ValidateAsync(request, ct);
        if (!validationResut.IsValid)
        {
            return BadRequest("Invalid credential given");
        }
        var token = await accessService.Login(request, ct);
        if(token is null)
        {
            return BadRequest("Invalid email or password given");
        }
        return Ok(token);
    }
}