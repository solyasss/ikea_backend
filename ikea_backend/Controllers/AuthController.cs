using Microsoft.AspNetCore.Mvc;
using ikea_business.DTO;
using ikea_business.Services.Interfaces;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _svc;

    public AuthController(IUserService svc) => _svc = svc;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInput dto)
    {
        var user = await _svc.AuthenticateAsync(dto.Email, dto.Password);
        if (user == null) return Unauthorized(new { message = "Invalid email or password" });

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

        return Ok(new { message = "Login successful" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { message = "Logout successful" });
    }

    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var userName = HttpContext.Session.GetString("UserName");

        if (userId == null)
            return Unauthorized(new { message = "Not logged in" });

        return Ok(new
        {
            UserId = userId,
            UserName = userName
        });
    }
}