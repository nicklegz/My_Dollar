using Extensions;
using Microsoft.AspNetCore.Authorization;
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
    [Produces("application/json")]
    [HttpPost("[controller]/sign-in")]
    public async Task<IActionResult> SignIn([FromBody] User user)
    {
        User existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if(existingUser == null)
        {
            return NotFound(String.Format($"Username {0} does not exist.", user.Username));
        }

        bool isValidPassword = await PasswordExtensions.IsValidPassword(user, _userRepository);
        if(!isValidPassword)
        {
            return Unauthorized("Invalid password. Please try again.");
        }

        string accessToken = _tokenService.GenerateToken(user);
        TokenResponse tokenResponse = new TokenResponse
        {
            access_token = accessToken,
            token_type = "Bearer",
            expires_in = 3600
        };

        return Ok(tokenResponse);
    }

    [AllowAnonymous]
    [Produces("application/json")]
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