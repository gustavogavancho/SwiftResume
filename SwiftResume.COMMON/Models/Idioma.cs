using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Idioma : BaseEntity
{
    [Required] public string Nombre { get; set; }
    [Required] public string Nivel { get; set; }
    public int ResumeId { get; set; }
}
