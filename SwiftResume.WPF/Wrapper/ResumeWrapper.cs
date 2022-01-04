using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper;

public class ResumeWrapper : ModelWrapper<Resume>
{
    public ResumeWrapper(Resume model) : base(model) { }
    public int Id { get { return Model.Id; } }

    public string Nombres
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Apellidos
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Genero
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Lenguaje
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string FotoString
    {
        get => GetValue<string>();
        set => SetValue(value);
    }


    protected override IEnumerable<Tuple<string, string>> ValidateProperty()
    {
        if (Nombres == "Gustavo")
        {
            Tuple<string, string> t = new(nameof(Nombres), "Gustavo es un nombre baneado momentaneamente.");
            yield return t;
        }

        if (Apellidos == "Gavancho")
        {
            Tuple<string, string> t = new(nameof(Apellidos), "Gavancho es un apellido baneado momentaneamente.");
            yield return t;
        }
    }

    protected override IEnumerable<string> ValidateProperty(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(Nombres):
                if (string.Equals(Nombres, "Gustavo", StringComparison.OrdinalIgnoreCase))
                    yield return "Gustavo es un nombre baneado momentaneamente.";
                break;
            case nameof(Apellidos):
                if (string.Equals(Apellidos, "Gavancho", StringComparison.OrdinalIgnoreCase))
                    yield return "Gavancho es un nombre baneado momentaneamente.";
                break;
        }
    }
}
