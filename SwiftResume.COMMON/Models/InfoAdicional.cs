using System.ComponentModel.DataAnnotations;

namespace SwiftResume.COMMON.Models;

public class InfoAdicional : BaseEntity
{
    [Required] public string TipoInfoAdicional { get; set; }
    [Required] public string Descripcion { get; set; }

    public int ResumeId { get; set; }
}