using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Security.Claims;

namespace Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _secretKey;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["MyDollar:UserKey"]));
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            }),

            Issuer = _configuration["TokenConfig:Issuer"],

            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(20)),

            SigningCredentials = new SigningCredentials(
                _secretKey, SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetClaimsPrincipal(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return null;
            }

            var symmetricKey = Convert.FromBase64String(_configuration["MyDollar:UserKey"]);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            return principal;
        }

        catch (Exception)
        {
            //should write log
            return null;
        }
    }
}