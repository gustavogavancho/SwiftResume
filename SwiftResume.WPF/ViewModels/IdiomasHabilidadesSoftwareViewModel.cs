using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Idioma;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
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
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly IdiomaDialogViewModel _idiomaDialogViewModel;

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

    private int _resumeId;
    public int ResumeId
    {
        get => _resumeId;
        set 
        {
            _resumeId = value;
            OnPropertyChanged(nameof(ResumeId));
        }
    }

    #endregion

    #region Commands

    public DelegateCommand AddIdiomaCommand { get; private set; }
    public DelegateCommand DeleteIdiomaCommand { get; private set; }

    #endregion

    #region Constructor

    public IdiomasHabilidadesSoftwareViewModel(IIdiomaRepository idiomaRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        AlertDialogViewModel alertDialogViewModel,
        IdiomaDialogViewModel idiomaDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _idiomaRepository = idiomaRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;
        _alertDialogViewModel = alertDialogViewModel;
        _idiomaDialogViewModel = idiomaDialogViewModel;
        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddIdiomaCommand = new DelegateCommand(OnAddIdioma);
        DeleteIdiomaCommand = new DelegateCommand(OnDeleteIdioma);
    }


    #endregion

    #region Methods

    public async void OnLoad()
    {
        
    }

    private async void OnDeleteIdioma()
    {
        if (Idioma is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el idioma: {Idioma.Nombre}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _idiomaRepository.Remove(Idioma);
                Idiomas.Remove(Idioma);

                await _idiomaRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }

        }
    }

    private void OnNavigateToEditResume(int Id)
    {
        ResumeId = Id;
        Idiomas = _idiomaRepository.Find(x => x.ResumeId == Id).ToObservableCollection();
    }

    private void OnAddIdioma()
    {
        _idiomaDialogViewModel.ResumeId = ResumeId;
        var idioma = _dialogService.OpenDialog(_idiomaDialogViewModel);

        if (idioma is not null)
        {
            Idiomas.Add(idioma);
        }
    }

    #endregion
}