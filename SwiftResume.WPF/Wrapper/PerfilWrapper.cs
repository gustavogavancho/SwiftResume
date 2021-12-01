using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class PerfilWrapper : ModelWrapper<Perfil>
{
    public PerfilWrapper(Perfil model) : base(model) { }

    public int Id { get { return Model.Id; } }

    public string Profesion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Resumen
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Direccion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Telefono
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Email
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Email2
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public DateTime FechaNacimiento
    {
        get => GetValue<DateTime>();
        set => SetValue(value);
    }

}