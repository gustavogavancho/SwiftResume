using SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.Wrapper
{
    public class InfoAdicionalWrapper : ModelWrapper<InfoAdicional>
    {
        public InfoAdicionalWrapper(InfoAdicional model) : base(model) {}

        public int Id { get { return Model.Id; } }

        public string TipoInfoAdicional
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
}
