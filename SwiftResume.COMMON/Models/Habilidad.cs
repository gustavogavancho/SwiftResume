using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Habilidad : BaseEntity
{
    [Required] public int Nombre { get; set; }
    [Required] public string Nivel { get; set; }
    public int ResumeId { get; set; }
}