using Microsoft.AspNetCore.Mvc;
using TraceFlowTraining.Application.DTOs.Auth;
using TraceFlowTraining.Infrastructure.Persistence.Context;
using TraceFlowTraining.Infrastructure.Security;

namespace TraceFlow_Training.Controllers;

/// <summary>
/// Authentication endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TraceFlowDbContext _context;

    private readonly TokenService _tokenService;

    private readonly PasswordService _passwordService;

    public AuthController(
        TraceFlowDbContext context,
        TokenService tokenService,
        PasswordService passwordService)
    {
        _context = context;

        _tokenService = tokenService;

        _passwordService = passwordService;
    }

    /// <summary>
    /// Authenticates a user and returns JWT token
    /// </summary>
    /// <param name="dto">Login credentials</param>
    /// <returns>JWT token</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Username == dto.Username);

        if (user is null)
        {
            return Unauthorized(new
            {
                message = "Invalid credentials"
            });
        }

        var passwordValid = _passwordService.VerifyPassword(
            dto.Password,
            user.Password);

        if (!passwordValid)
        {
            return Unauthorized(new
            {
                message = "Invalid credentials"
            });
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token
        });
    }

    [HttpPost("generate-hash")]
    public IActionResult GenerateHash([FromBody] string password)
    {
        var hash = _passwordService.HashPassword(password);

        return Ok(new
        {
            hash
        });
    }
}