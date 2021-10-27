using SwiftResume.COMMON.Models;
using System;

namespace SwiftResume.WPF.State.Users
{
    public interface IUserStored
    {
        User CurrentUser { get; set; }
        event Action StateChanged;
    }
}
