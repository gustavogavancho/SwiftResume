using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;

namespace SwiftResume.WPF.ViewModels;

public class PerfilViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IResumeRepository _resumeRepository;
    private readonly IDialogService _dialogService;
    private readonly AlertDialogViewModel _alertDialogViewModel;

    #endregion

    #region Properties

    public string Name { get; set; } = "Perfil";


    private Resume _resume;
    public Resume Resume
    {
        get => _resume;
        set
        {
            _resume = value;
            OnPropertyChanged(nameof(Resume));
            OnPropertyChanged(nameof(PerfilWrapper));
        }
    }

    private PerfilWrapper _perfilWrapper;
    public PerfilWrapper PerfilWrapper
    {
        get => _perfilWrapper;
        set
        {
            _perfilWrapper = value;
            OnPropertyChanged(nameof(PerfilWrapper));
        }
    }

    private bool _hasChanges;
    public bool HasChanges
    {
        get => _hasChanges;
        set
        {
            _hasChanges = value;
            OnPropertyChanged(nameof(HasChanges));
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    #endregion

    #region Commands

    public DelegateCommand SaveCommand { get; set; }

    #endregion

    #region Constuctor
    public PerfilViewModel(IEventAggregator eventAggregator,
        IResumeRepository resumeRepository,
        IDialogService dialogService,
        AlertDialogViewModel alertDialogViewModel)
    {
        _resumeRepository = resumeRepository;
        _dialogService = dialogService;
        _alertDialogViewModel = alertDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        SaveCommand = new DelegateCommand(OnSave, CanSave);
    }


    #endregion

    #region Methods

    public void OnLoad()
    {
        InitializePerfil(Resume.Perfil);

        //Restore has changes to false
        HasChanges = false;
    }

    private bool CanSave()
    {
        return PerfilWrapper != null && !PerfilWrapper.HasErrors && HasChanges;
    }

    private async void OnSave()
    
    {
        PerfilWrapper.Validate();
        if (!PerfilWrapper.HasErrors)
        {
            //Workaround
            Resume.Nombres = PerfilWrapper.Nombres;
            Resume.Apellidos = PerfilWrapper.Apellidos;
            Resume.Perfil = PerfilWrapper.Model;

            await _resumeRepository.SaveAsync();
            HasChanges = _resumeRepository.HasChanges();
            _alertDialogViewModel.Message = "Se guardaron los cambios correctamente.";
            _dialogService.OpenDialog(_alertDialogViewModel);
        }
    }

    private async void OnNavigateToEditResume(int id)
    {
        Resume = await _resumeRepository.GetResumeWithProfile(id);

        if (Resume?.Perfil == null)
            Resume.Perfil = new Perfil();
    }

    private void InitializePerfil(Perfil perfil)
    {
        PerfilWrapper = new PerfilWrapper(perfil);
        PerfilWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _resumeRepository.HasChanges();

            if (e.PropertyName == nameof(PerfilWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();

        //Workaround
        PerfilWrapper.Nombres = Resume.Nombres;
        PerfilWrapper.Apellidos = Resume.Apellidos;
    }
    
    public void OnSelectionChanged()
    {
        HasChanges = true;
    }

    #endregion
}
