using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class ProyectoWrapper : ModelWrapper<Proyecto>
{
    public ProyectoWrapper(Proyecto model) : base(model) {}

    public int Id { get { return Model.Id; } }

    public string TipoProyecto
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Titulo
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Link
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Descripcion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}