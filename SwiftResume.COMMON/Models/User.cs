namespace SwiftResume.COMMON.Models;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateJoined { get; set; }
}
