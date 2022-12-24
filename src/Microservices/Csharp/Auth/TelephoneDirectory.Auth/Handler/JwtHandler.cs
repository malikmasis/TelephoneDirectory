using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TelephoneDirectory.Auth.Interfaces;

namespace TelephoneDirectory.Auth.Handler;

public sealed class JwtHandler : IJwtHandler
{
    private readonly IConfiguration _config;

    public JwtHandler(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJSONWebToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: new List<Claim>(), // claims (are used to filter the data)
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: signCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
