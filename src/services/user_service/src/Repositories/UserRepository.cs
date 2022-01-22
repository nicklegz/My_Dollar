using Extensions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<CreateUserResponse> CreateAsync(User user)
    {
        var result = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new CreateUserResponse(result.Entity.Id, result.Entity.Username);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}