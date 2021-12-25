using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper
{
    public class HabilidadWrapper : ModelWrapper<Habilidad>
    {
        public HabilidadWrapper(Habilidad model) : base(model) {}

        public int Id { get { return Model.Id; } }

        public string Tipo
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

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
