using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Tab;

namespace SwiftResume.WPF.ViewModels;

public class EducacionExperienciaViewModel : ViewModelBase, ITab
{
    public string Name { get; set; } = "Educación/Experiencia";
}