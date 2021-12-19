using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper
{
    public class IdiomaWrapper : ModelWrapper<Idioma>
    {
        public IdiomaWrapper(Idioma model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Nombre
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Nivel
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
    }
}
