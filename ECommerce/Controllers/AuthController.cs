using Ecommerce.Repositories.Collections;
using Ecommerce.Repositories.Interfaces;
using ECommerce;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserCollection _userRepository;

    public AuthController(IUserCollection userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
         var user = await _userRepository.GetUserByUserName(model.UserName);

    if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSecretKey)), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    return Unauthorized();
    }
}
