namespace SwiftResume.COMMON.Models;

public class Educacion : BaseEntity
{
    public string Institucion { get; set; }
    public string Descripcion { get; set; }
    public string Meritos { get; set; }
    public DateTime FechaInicio { get; set; } = DateTime.Now;
    public DateTime FechaFin { get; set; } = DateTime.Now;
}
