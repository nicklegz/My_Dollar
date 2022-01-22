using Models;

namespace Services;

public interface ITokenService
{
    public string GenerateToken(User user);
}