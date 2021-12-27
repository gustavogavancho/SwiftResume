using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class EducacionWrapper : ModelWrapper<Educacion>
{
    public EducacionWrapper(Educacion model) : base(model) {}

    public int Id { get { return Model.Id; } }

    public string Institucion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string TipoEducacion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }


    public string Descripcion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Meritos
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
}