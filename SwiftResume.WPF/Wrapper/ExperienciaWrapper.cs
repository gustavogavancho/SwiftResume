using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class ExperienciaWrapper : ModelWrapper<Experiencia>
{
    public ExperienciaWrapper(Experiencia model) : base(model) {}
    public int Id { get { return Model.Id; } }

    public string Institucion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Descripcion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Lugar
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Responsabilidades
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Logros
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public DateTime FechaInicio
    {
        get => GetValue<DateTime>();
        set => SetValue(value);
    }

    public DateTime FechaFin
    {
        get => GetValue<DateTime>();
        set => SetValue(value);
    }

    public bool Mostrar
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    public bool EsActual
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
}