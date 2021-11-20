using Microsoft.AspNetCore.Identity;
using SwiftResume.BIZ.Exceptions;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;

namespace SwiftResume.BIZ.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenticationService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> Login(string username, string password)
    {
        User user = await _userRepository.GetByUserName(username);

        if (user is null)
        {
            throw new UserNotFoundException($"El nombre de usuario {username} no existe.");
        }
        PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHashed, password);

        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new InvalidPasswordException("Clave incorrecta, por favor intenta de nuevo.");
        }

        return user;
    }
}
