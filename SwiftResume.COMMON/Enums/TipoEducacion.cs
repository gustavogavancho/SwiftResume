using System.ComponentModel;

namespace SwiftResume.COMMON.Enums;

public enum TipoEducacion : short
{
    [Description("Bachiller")]
    Bachiller,
    [Description("Titulo Profesional")]
    TituloProfesional,
    [Description("Maestria")]
    Maestria,
    [Description("Doctorado")]
    Doctorado,
    [Description("Diplomado")]
    Diplomado,
    [Description("Segunda Especialidad")]
    SegundaEspecialidad
}