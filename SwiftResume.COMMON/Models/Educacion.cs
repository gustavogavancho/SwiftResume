using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Educacion : BaseEntity
{
    [Required] public string Institucion { get; set; }
    [Required] public string Descripcion { get; set; }
    public string Meritos { get; set; }
    [Required] public DateTime FechaInicio { get; set; } = DateTime.Now;
    [Required] public DateTime FechaFin { get; set; } = DateTime.Now;
    public int ResumeId { get; set; }
}
