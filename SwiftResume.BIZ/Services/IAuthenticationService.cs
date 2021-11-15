using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;

namespace SwiftResume.BIZ.Services;

public interface IAuthenticationService
{
    Task<RegistrationResult> Register(string email, string username, string password, string confirmpassword);
    Task<User> Login(string username, string password);
}