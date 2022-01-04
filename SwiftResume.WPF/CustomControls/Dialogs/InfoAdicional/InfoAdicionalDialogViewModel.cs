using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.InfoAdicional;

public class InfoAdicionalDialogViewModel : DialogViewModelBase<Model.InfoAdicional>
{
    #region Fields

    private readonly IInfoAdicionalRepository _infoAdicionalRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private Model.InfoAdicional _infoAdicionalEdit;

    #endregion

    #region Properties

    public int ResumeId { get; set; }

    private InfoAdicionalWrapper _infoAdicionalWrapper;
    public InfoAdicionalWrapper InfoAdicionalWrapper
    {
        get => _infoAdicionalWrapper;
        set
        {
            _infoAdicionalWrapper = value;
            OnPropertyChanged(nameof(InfoAdicionalWrapper));
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

    public InfoAdicionalDialogViewModel(IInfoAdicionalRepository infoAdicionalRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _infoAdicionalRepository = infoAdicionalRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditInfoAdicional>()
            .Subscribe(OnNavigateToEditInfoAdicional);

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
    }

    #endregion

    #region Methods

    private bool CanSave(IDialogWindow arg)
    {
        return InfoAdicionalWrapper != null && !InfoAdicionalWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        InfoAdicionalWrapper.Validate();
        if (!InfoAdicionalWrapper.HasErrors)
        {
            _infoAdicionalRepository.SaveAsync();
            HasChanges = _infoAdicionalRepository.HasChanges();
            CloseDialogWithResult(window, InfoAdicionalWrapper.Model);
        }
    }


    private async void OnNavigateToEditInfoAdicional(int infoAdicionalId)
    {
        _infoAdicionalEdit = await _infoAdicionalRepository.Get(infoAdicionalId);
    }

    public void OnLoad() 
    {
        //Restore has changes to false
        HasChanges = false;

        var infoAdicional = (_infoAdicionalEdit is not null) ?
            _infoAdicionalEdit
            : CreateNewInfoAdicional();

        InitializeInfoAdicional(infoAdicional);
    }

    private void InitializeInfoAdicional(Model.InfoAdicional infoAdicional)
    {
        InfoAdicionalWrapper = new InfoAdicionalWrapper(infoAdicional);
        InfoAdicionalWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _infoAdicionalRepository.HasChanges();

            if (e.PropertyName == nameof(InfoAdicionalWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();
    }

    private Model.InfoAdicional CreateNewInfoAdicional()
    {
        var infoAdicional = new Model.InfoAdicional();
        infoAdicional.ResumeId = ResumeId;
        _infoAdicionalRepository.Add(infoAdicional);
        return infoAdicional;
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

        _infoAdicionalRepository.DetachAllProperties();
        _infoAdicionalEdit = null;
        CloseDialogWithResult(window, null);
    }
    #endregion
}
