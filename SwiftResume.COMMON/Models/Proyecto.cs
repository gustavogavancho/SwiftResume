using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class Proyecto : BaseEntity
{
    [Required] public string TipoProyecto { get; set; }
    [Required] public string Titulo { get; set; }
    [Required] public string Link { get; set; }
    [Required] public string Descripcion { get; set; }

    public int ResumeId { get; set; }
}