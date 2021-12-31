using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Tab;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class EditViewModel : ViewModelBase
{
    #region Fields

    public ICollection<ITab> Tabs { get; set; } = new ObservableCollection<ITab>();
    public ITab Tab { get; set; }

    #endregion

    #region Constructor

    public EditViewModel(PerfilViewModel perfilViewModel, 
        IdiomasHabilidadesSoftwareViewModel idiomasHabilidadesSoftwareViewModel,
        EducacionExperienciaViewModel educacionExperienciaViewModel,
        CertificacionProyectoViewModel certificacionViewModel,
        InfoAdicionalViewModel infoAdicionalViewModel)
    {
        Tabs.Add(perfilViewModel);
        Tabs.Add(idiomasHabilidadesSoftwareViewModel);
        Tabs.Add(educacionExperienciaViewModel);
        Tabs.Add(certificacionViewModel);
        Tabs.Add(infoAdicionalViewModel);
        Tab = Tabs.FirstOrDefault();
    }

    #endregion
}
