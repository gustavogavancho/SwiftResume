using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.State.Authenticators;

public interface IAuthenticator
{
    User CurrentUser { get; }
    bool IsLoggedIn { get; }
    event Action StateChanged;
    Task<RegistrationResult> Register(string email, string username, string password, string confirmpassword);
    Task Login(string username, string password);
    void Logout();
}