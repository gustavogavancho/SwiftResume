using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.Wrapper;

namespace SwiftResume.WPF.ViewModels;

public class EditViewModel : ViewModelBase
{
    #region Fields

    private readonly ViewModelDelegateRenavigator<ResumeViewModel> _resumeRenavigator;
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
        }
    }

    #endregion

    #region Commands
    public DelegateCommand ReturnCommand { get; private set; }
    public DelegateCommand SaveCommand { get; set; }
    #endregion

    #region Constructor

    public EditViewModel(ViewModelDelegateRenavigator<ResumeViewModel> resumeRenavigator,
        IEventAggregator eventAggregator,
        IResumeRepository resumeRepository)
    {
        _resumeRenavigator = resumeRenavigator;
        _resumeRepository = resumeRepository;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        ReturnCommand = new DelegateCommand(OnReturn);
        SaveCommand = new DelegateCommand(OnSave, CanSave);
    }


    private async void OnSave()
    {
        PerfilWrapper.Validate();
        if (!PerfilWrapper.HasErrors)
        {
            await _resumeRepository.SaveAsync();
        }
    }

    private bool CanSave()
    {
        return PerfilWrapper != null && !PerfilWrapper.HasErrors;
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

        if (Resume.Perfil == null)
            Resume.Perfil = new Perfil();
    }

    private void OnReturn()
    {
        _resumeRenavigator.Renavigate();
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
    }


    #endregion
}
