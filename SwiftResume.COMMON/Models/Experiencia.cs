using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Experiencia : BaseEntity
{
    [Required] public string Institucion { get; set; }
    [Required] public string Descripcion { get; set; }
    [Required] public string Lugar { get; set; }
    [Required] public string Responsabilidades { get; set; }
    public string Logros { get; set; }
    [Required] public DateTime FechaInicio { get; set; } = DateTime.Now;
    [Required] public DateTime FechaFin { get; set; } = DateTime.Now;
    public int ResumeId { get; set; }

}