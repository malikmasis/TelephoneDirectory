using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.WebApi.Extentions;

public static class JwtAddExtension
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // validate the server
                    ValidateAudience = true, // Validate the recipient of token is authorized to receive
                    ValidateLifetime = true, // Check if token is not expired and the signing key of the issuer is valid 
                    ValidateIssuerSigningKey = true, // Validate signature of the token 

                    //Issuer and audience values are same as defined in generating Token
                    ValidIssuer = config.GetSection("Jwt")["Issuer"].ToString(), // stored in appsetting file
                    ValidAudience = config["Jwt:Issuer"], // stored in appsetting file
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])) // stored in appsetting file
                };
            });
    }
}
