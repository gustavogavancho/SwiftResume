using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class CertificacionWrapper : ModelWrapper<Certificacion>
{
    public CertificacionWrapper(Certificacion model) : base(model) {}

    public int Id { get { return Model.Id; } }

    public string TipoCertificado
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string TipoActividad
    {
        get => GetValue<string>();
        set => SetValue(value);
    }


    public string Descripcion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Institucion
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Ponente
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public int Horas
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public DateTime Fecha
    {
        get => GetValue<DateTime>();
        set => SetValue(value);
    }
}