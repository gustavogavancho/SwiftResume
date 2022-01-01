using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Certificacion : BaseEntity
{
    [Required] public string TipoCertificado { get; set; }
    [Required] public string TipoActividad { get; set; }
    [Required] public string Descripcion { get; set; }
    [Required] public string Institucion { get; set; }
    [Required] public string Ponente { get; set; }
    [Required] public int Horas { get; set; }
    [Required] public DateTime Fecha { get; set; } = DateTime.Now;
    public int ResumeId { get; set; }
}