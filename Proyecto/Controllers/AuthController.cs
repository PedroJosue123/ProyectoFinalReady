using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.ICaseUse;
using Domain.Dtos;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
 
namespace Proyecto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginUser _loginUser;
    private readonly IRegisterUser _registerUser;

    public AuthController(ILoginUser loginUser, IRegisterUser registerUser)
    {
        _loginUser = loginUser;
        _registerUser = registerUser;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        try
        {
            var token = await _loginUser.Execute(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        try
        {
            var result = await _registerUser.Execute(dto);
            return Ok(new { registered = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}