using Microsoft.AspNetCore.Identity;
using Models;
using Repositories.Interfaces;

namespace Extensions;

public static class PasswordExtensions
{
    private static readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
    public async static Task<bool> IsValidPassword(User user, IUserRepository userRepository)
    {
        string hashedPassword = passwordHasher.HashPassword(user, user.Password);
        PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, hashedPassword, user.Password);
        bool isValidPassword = false;

        switch (passwordVerificationResult)
        {
            case PasswordVerificationResult.Failed:
                break;

            case PasswordVerificationResult.SuccessRehashNeeded:
                PasswordExtensions.HashUserPassword(user);
                await userRepository.UpdateAsync(user);
                isValidPassword = true;
                break;

            case PasswordVerificationResult.Success:
                isValidPassword = true;
                break;
        }

        return isValidPassword;
    }
    
    //hashes a password into itself
    public static User HashUserPassword(User user)
    {
        user.Password = passwordHasher.HashPassword(user, user.Password);
        return user;
    }
}