using SwiftResume.COMMON.CustomValidationAtributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftResume.COMMON.Models;

public class Perfil : BaseEntity
{
    [Required, MinLength(2), NotMapped] public string Nombres { get; set; }
    [Required, MinLength(2), NotMapped] public string Apellidos { get; set; }
    [Required, MinLength(2)] public string Profesion { get; set; }
    [Required, MinLength(120)] public string Resumen { get; set; }
    [Required, MinLength(5)] public string Direccion { get; set; }
    [Required, Phone] public string Telefono { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    [NotMapped] public string FotoString { get; set; }
    public string Email2 { get; set; }
    [Required, CheckDateRangeAttribute] public DateTime FechaNacimiento { get; set; } = DateTime.Now;
    public string Linkedin { get; set; }
    public string Github { get; set; }
    public string Blog { get; set; }
    public string StackOverflow { get; set; }

    public int ResumeId { get; set; }
}