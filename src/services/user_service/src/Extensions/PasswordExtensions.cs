using Microsoft.AspNetCore.Identity;
using Models;

namespace Extensions;

public static class PasswordExtensions
{
    private static readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
    public static PasswordVerificationResult IsValidPassword(User user)
    {
        string hashedPassword = passwordHasher.HashPassword(user, user.Password);
        return passwordHasher.VerifyHashedPassword(user, hashedPassword, user.Password);
    }
    
    //hashes a password into itself
    public static User HashUserPassword(User user)
    {
        user.Password = passwordHasher.HashPassword(user, user.Password);
        return user;
    }
}