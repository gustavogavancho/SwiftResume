using SwiftResume.BIZ.Services;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.State.Users;

namespace SwiftResume.WPF.State.Authenticators;

public class Authenticator : IAuthenticator
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserStored _userStored;

    public Authenticator(IAuthenticationService authenticationService, IUserStored userStored)
    {
        _authenticationService = authenticationService;
        _userStored = userStored;
    }

    public User CurrentUser
    {
        get => _userStored.CurrentUser;
        private set
        {
            _userStored.CurrentUser = value;
            StateChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentUser != null;

    public event Action StateChanged;

    public async Task Login(string username, string password)
    {
        CurrentUser = await _authenticationService.Login(username, password);
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}