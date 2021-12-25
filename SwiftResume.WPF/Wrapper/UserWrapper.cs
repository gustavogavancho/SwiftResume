using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class UserWrapper : ModelWrapper<User>
{
    public UserWrapper(User model) : base(model) {}

    public int Id { get { return Model.Id; } }

    public string Email
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Username
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Password
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string ConfirmPassword
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}
