using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;

namespace SwiftResume.WPF.ViewModels;

public class PerfilViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IResumeRepository _resumeRepository;

    #endregion

    #region Properties

    private Resume _resume;
    public Resume Resume
    {
        get => _resume;
        set
        {
            _resume = value;
            OnPropertyChanged(nameof(Resume));
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
    public string Name { get; set; }

    #endregion

    #region Commands
    public DelegateCommand SaveCommand { get; set; }

    #endregion

    #region Constuctor
    public PerfilViewModel(IEventAggregator eventAggregator,
        IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        SaveCommand = new DelegateCommand(OnSave, CanSave);
    }

    #endregion

    #region Methods
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
            Resume.Apellidos = PerfilWrapper.Nombres;
            Resume.Perfil = PerfilWrapper.Model;

            await _resumeRepository.SaveAsync();
        }
    }

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;

        InitializePerfil(Resume.Perfil);
    }

    private async void OnNavigateToEditResume(NavigateToEditResumeArgs model)
    {
        Resume = await _resumeRepository.GetResumeWithProfile(model.Id);

        if (Resume?.Perfil == null)
            Resume.Perfil = new Perfil();
    }

    private void InitializePerfil(Perfil perfil)
    {
        Name = "Perfil";
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

    #endregion
}
