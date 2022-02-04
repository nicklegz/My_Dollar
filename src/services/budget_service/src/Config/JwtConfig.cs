using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Config;

public static class JwtConfig
{
    private static TokenValidationParameters _tokenValidationParameters;

    public static TokenValidationParameters TokenValidationParameters
    {
        get { return _tokenValidationParameters ?? null;}
    }

    public static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        SetTokenValidationParameters(configuration);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = _tokenValidationParameters;
        });
    }

    private static void SetTokenValidationParameters(IConfiguration configuration)
    {
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = configuration["TokenConfig:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["MyDollar:UserKey"])),
            ClockSkew = TimeSpan.Zero
        };
    }
}