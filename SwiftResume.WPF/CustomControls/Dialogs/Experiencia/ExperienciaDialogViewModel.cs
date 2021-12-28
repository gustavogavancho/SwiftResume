using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;


namespace SwiftResume.WPF.CustomControls.Dialogs.Experiencia;

public class ExperienciaDialogViewModel : DialogViewModelBase<Model.Experiencia>
{
    #region Fields

    private readonly IExperienciaRepository _experienciaRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private Model.Experiencia _experienciaEdit;

    #endregion

    #region Properties

    public int ResumeId { get; set; }

    private ExperienciaWrapper _experienciaWrapper;
    public ExperienciaWrapper ExperienciaWrapper
    {
        get => _experienciaWrapper;
        set
        {
            _experienciaWrapper = value;
            OnPropertyChanged(nameof(ExperienciaWrapper));
        }
    }

    private bool _hasChanges;
    public bool HasChanges
    {
        get => _hasChanges;
        set
        {
            if (_hasChanges != value)
            {
                _hasChanges = value;
                OnPropertyChanged(nameof(HasChanges));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
    }

    #endregion

    #region Commands

    public DelegateCommand<IDialogWindow> SaveCommand { get; private set; }
    public DelegateCommand<IDialogWindow> CancelCommand { get; private set; }

    #endregion

    #region Constructor

    public ExperienciaDialogViewModel(IExperienciaRepository experienciaRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _experienciaRepository = experienciaRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditExperiencia>()
                .Subscribe(OnNavigateToEditExperiencia);

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
    }

    #endregion

    #region Methods

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;

        var experiencia = (_experienciaEdit is not null) ?
            _experienciaEdit
            : CreateNewIdioma();

        InitializeResume(experiencia);
    }

    private async void OnNavigateToEditExperiencia(int experienciaId)
    {
        _experienciaEdit = await _experienciaRepository.Get(experienciaId);
    }

    private void OnCancel(IDialogWindow window)
    {
        if (HasChanges)
        {
            _yesNoDialogViewModel.Message = $"Hay cambios pendientes, al cerrar la ventana se borrarán los cambios, ¿Desea cerrar la ventana?";
            var dialog = _dialogService.OpenDialog(_yesNoDialogViewModel);
            if (dialog == DialogResults.No)
                return;
        }

        _experienciaRepository.DetachAllProperties();
        _experienciaEdit = null;
        CloseDialogWithResult(window, null);
    }


    private bool CanSave(IDialogWindow arg)
    {
        return ExperienciaWrapper != null && !ExperienciaWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        ExperienciaWrapper.Validate();
        if (!ExperienciaWrapper.HasErrors)
        {
            _experienciaRepository.SaveAsync();
            HasChanges = _experienciaRepository.HasChanges();
            CloseDialogWithResult(window, ExperienciaWrapper.Model);
        }
    }

    private void InitializeResume(Model.Experiencia experiencia)
    {
        ExperienciaWrapper = new ExperienciaWrapper(experiencia);
        ExperienciaWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _experienciaRepository.HasChanges();

            if (e.PropertyName == nameof(ExperienciaWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();
    }

    private Model.Experiencia CreateNewIdioma()
    {
        var experiencia = new Model.Experiencia();
        experiencia.ResumeId = ResumeId;
        _experienciaRepository.Add(experiencia);
        return experiencia;
    }

    #endregion
}