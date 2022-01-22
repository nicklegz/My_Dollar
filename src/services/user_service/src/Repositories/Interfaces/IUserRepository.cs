using Models;

namespace Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<CreateUserResponse> CreateAsync(User user);
    Task UpdateAsync(User user);   
}
