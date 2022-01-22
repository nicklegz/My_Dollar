using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Repositories.Interfaces;
using Services;

namespace Controllers;

[ApiController]
[Route("api/")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public UserController(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    [AllowAnonymous]
    [HttpPost("[controller]/sign-in")]
    public async Task<IActionResult> SignIn([FromBody] User user)
    {
        User existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if(existingUser == null)
        {
            return NotFound(String.Format($"Username {0} does not exist.", user.Username));
        }

        PasswordVerificationResult isValidPassword = PasswordExtensions.IsValidPassword(user);
        switch(isValidPassword)
        {
            case PasswordVerificationResult.Failed:
                return Unauthorized("Invalid password. Please try again.");
            
            case PasswordVerificationResult.SuccessRehashNeeded:
                PasswordExtensions.HashUserPassword(user);
                await _userRepository.UpdateAsync(user);
                break;
            
            case PasswordVerificationResult.Success:
            break;
        }

        return Ok(_tokenService.GenerateToken(user));
    }

    [AllowAnonymous]
    [HttpPost("[controller]/sign-up")]
    public async Task<IActionResult> SignUp([FromBody] User user)
    {
        User existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if(existingUser != null)
        {
            return Conflict();
        }

        PasswordExtensions.HashUserPassword(user);
        CreateUserResponse result = await _userRepository.CreateAsync(user);
        return CreatedAtAction(nameof(SignUp), result);
    }
}