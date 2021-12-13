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

    public EditViewModel(PerfilViewModel perfilViewModel, IdiomasHabilidadesSoftwareViewModel idiomasHabilidadesSoftwareViewModel)
    {
        Tabs.Add(perfilViewModel);
        Tabs.Add(idiomasHabilidadesSoftwareViewModel);
        Tab = Tabs.FirstOrDefault();
    }

    #endregion
}
