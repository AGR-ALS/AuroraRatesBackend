using AuroraRates.Api.Contracts;
using AuroraRates.Domain.Abstractions.Users;
using Microsoft.AspNetCore.Mvc;

namespace AuroraRates.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
    {
        await _userService.Register(userRegisterRequest.Nickname, userRegisterRequest.Password, userRegisterRequest.Email);
        
        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        var token = await _userService.Login(userLoginRequest.Email, userLoginRequest.Password);
        
        AddToken(token);
        
        return Results.Ok(token);
    }
    

    [HttpGet("isAuth")]
    public async Task<IResult> CheckIfUserIsAuth()
    {
        return Results.Ok(User.Identity != null && (User.Identity.IsAuthenticated));
    }

    [HttpGet("getUsername")]
    public async Task<IResult> GetCurrentUserId()
    {
        var userNickname = User.FindFirst("Nickname")?.Value;
        return Results.Ok(userNickname);
    }
    
    [HttpPost("logout")]
    public async Task<IResult> Logout()
    {
        if (Request.Cookies.ContainsKey("token"))
        {
            Response.Cookies.Delete("token");
        }

        return Results.Ok();
    }

    private void AddToken(string token)
    {
        HttpContext.Response.Cookies.Append("token", token, new CookieOptions
        {
            SameSite = SameSiteMode.None,
            Secure = true
        });
    }
}