using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class ServiceRegisteration
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors();

        services.AddIdentityServices(config);

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts => 
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

}
