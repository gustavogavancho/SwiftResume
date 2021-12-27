using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Educacion;

public class EducacionDialogViewModel : DialogViewModelBase<Model.Educacion>
{
    #region Fields

    private readonly IEducacionRepository _educacionRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private Model.Educacion _educacionEdit;

    #endregion

    #region Properties

    public int ResumeId { get; set; }

    private EducacionWrapper _educacionWrapper;
    public EducacionWrapper EducacionWrapper
    {
        get => _educacionWrapper;
        set
        {
            _educacionWrapper = value;
            OnPropertyChanged(nameof(EducacionWrapper));
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

    public EducacionDialogViewModel(IEducacionRepository educacionRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        IEventAggregator eventAgreggator)
    {
        _educacionRepository = educacionRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAgreggator.GetEvent<NavigateToEditEducacion>()
                .Subscribe(OnNavigateToEditEducacion);

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
    }

    #endregion

    #region Methods

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;

        var educacion = (_educacionEdit is not null) ?
            _educacionEdit
            : CreateNewIdioma();

        InitializeResume(educacion);
    }

    private void OnNavigateToEditEducacion(Model.Educacion educacion)
    {
        _educacionEdit = educacion;
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

        _educacionRepository.DetachAllProperties();
        _educacionEdit = null;
        CloseDialogWithResult(window, null);
    }

    private bool CanSave(IDialogWindow arg)
    {
        return EducacionWrapper != null && !EducacionWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        EducacionWrapper.Validate();
        if (!EducacionWrapper.HasErrors)
        {
            _educacionRepository.SaveAsync();
            HasChanges = _educacionRepository.HasChanges();
            CloseDialogWithResult(window, EducacionWrapper.Model);
        }
    }

    private void InitializeResume(Model.Educacion educacion)
    {
        EducacionWrapper = new EducacionWrapper(educacion);
        EducacionWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _educacionRepository.HasChanges();

            if (e.PropertyName == nameof(EducacionWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();
    }

    private Model.Educacion CreateNewIdioma()
    {
        var educacion = new Model.Educacion();
        educacion.ResumeId = ResumeId;
        _educacionRepository.Add(educacion);
        return educacion;
    }

    #endregion
}