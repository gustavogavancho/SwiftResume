namespace SwiftResume.COMMON.Models;

public class Experiencia : BaseEntity
{
    public string Institucion { get; set; }
    public string Descripcion { get; set; }
    public string Lugar { get; set; }
    public string Responsabilidades { get; set; }
    public string Logros { get; set; }
    public DateTime FechaInicio { get; set; } = DateTime.Now;
    public DateTime FechaFin { get; set; } = DateTime.Now;

}