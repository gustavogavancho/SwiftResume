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
        PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new InvalidPasswordException("Clave incorrecta, por favor intenta de nuevo.");
        }

        return user;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmpassword)
    {
        RegistrationResult result = RegistrationResult.Success;

        if (password != confirmpassword)
        {
            result = RegistrationResult.PasswordsNoDotMatch;
        }

        User emailUser = await _userRepository.GetByEmail(email);

        if (emailUser != null)
        {
            result = RegistrationResult.EmailAlreadyExists;
        }

        User userName = await _userRepository.GetByUserName(username);

        if (userName != null)
        {
            result = RegistrationResult.UsernameAlreadyExists;
        }

        if (result == RegistrationResult.Success)
        {
            User user = new()
            {
                Email = email,
                Username = username,
                DateJoined = DateTime.Now,
            };

            string hashedPassword = _passwordHasher.HashPassword(user, password);

            user.PasswordHash = hashedPassword;

            await _userRepository.Add(user);
        }

        return result;
    }
}
