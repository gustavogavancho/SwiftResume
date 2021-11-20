using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;

namespace SwiftResume.BIZ.Services;

public interface IAuthenticationService
{
    Task<User> Login(string username, string password);
}