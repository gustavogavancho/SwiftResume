using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.State.Users;

public interface IUserStored
{
    User CurrentUser { get; set; }
    event Action StateChanged;
}