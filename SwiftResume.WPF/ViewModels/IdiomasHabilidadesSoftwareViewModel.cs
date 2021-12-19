using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Idioma;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Extensions;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class IdiomasHabilidadesSoftwareViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IIdiomaRepository _idiomaRepository;
    private readonly IDialogService _dialogService;
    private readonly IdiomaDialogViewModel _idiomaDialogViewModel;
    private readonly IEventAggregator _eventAggregator;

    #endregion

    #region Properties

    public string Name { get; set; } = "Idiomas/Habilidades/Software";

    private ObservableCollection<Idioma> _idiomas = new();
    public ObservableCollection<Idioma> Idiomas
    {
        get => _idiomas;
        set 
        {
            _idiomas = value;
            OnPropertyChanged(nameof(Idiomas));
        }
    }

    private Idioma _idioma;
    public Idioma Idioma
    {
        get => _idioma;
        set 
        {
            _idioma = value;
            OnPropertyChanged(nameof(Idioma));
        }
    }

    #endregion

    #region Commands

    public DelegateCommand AddIdiomaCommand { get; private set; }

    #endregion

    #region Constructor

    public IdiomasHabilidadesSoftwareViewModel(IIdiomaRepository idiomaRepository,
        IDialogService dialogService,
        IdiomaDialogViewModel idiomaDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _idiomaRepository = idiomaRepository;
        _dialogService = dialogService;
        _idiomaDialogViewModel = idiomaDialogViewModel;
        _eventAggregator = eventAggregator;
        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddIdiomaCommand = new DelegateCommand(OnAddIdioma);
    }

    private void OnNavigateToEditResume(int Id)
    {
        Idiomas = _idiomaRepository.Find(x => x.ResumeId == Id).ToObservableCollection();
    }

    private void OnAddIdioma()
    {
        var idioma = _dialogService.OpenDialog(_idiomaDialogViewModel);

        if (idioma is not null)
        {
            Idiomas.Add(idioma);
        }
    }

    #endregion

    #region Methods

    public async void OnLoad()
    {
        
    }

    #endregion
}