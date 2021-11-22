using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftResume.COMMON.Models;

public class User : BaseEntity
{
    [Required ,EmailAddress] public string Email { get; set; }
    [Required, MinLength(2)] public string Username { get; set; }
    [Required, MinLength(2), NotMapped] public string Password { get; set; }
    [Required, Compare(nameof(Password)), NotMapped] public string ConfirmPassword { get; set; }
    public string PasswordHashed { get; set; }
    public DateTime DateJoined { get; set; } = DateTime.Now;
    public Perfil Perfil { get; set; }
}
