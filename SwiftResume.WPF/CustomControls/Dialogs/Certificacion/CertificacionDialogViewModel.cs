using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Certificacion;

public class CertificacionDialogViewModel : DialogViewModelBase<Model.Certificacion>
{
    #region Fields

    private readonly ICertificacionRepository _certificacionRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private Model.Certificacion _certificacionEdit;

    #endregion

    #region Properties

    public int ResumeId { get; set; }

    private CertificacionWrapper _certificacionWrapper;
    public CertificacionWrapper CertificacionWrapper
    {
        get => _certificacionWrapper;
        set
        {
            _certificacionWrapper = value;
            OnPropertyChanged(nameof(CertificacionWrapper));
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

    public CertificacionDialogViewModel(ICertificacionRepository certificacionRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _certificacionRepository = certificacionRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditCertificacion>()
            .Subscribe(OnNavigateToEditCertificacion);

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
    }

    #endregion

    #region Methods

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;

        var certificacion = (_certificacionEdit is not null) ?
            _certificacionEdit
            : CreateNewCertificacion();

        InitializeCertificacion(certificacion);
    }

    private bool CanSave(IDialogWindow arg)
    {
        return CertificacionWrapper != null && !CertificacionWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        CertificacionWrapper.Validate();
        if (!CertificacionWrapper.HasErrors)
        {
            _certificacionRepository.SaveAsync();
            HasChanges = _certificacionRepository.HasChanges();
            CloseDialogWithResult(window, CertificacionWrapper.Model);
        }
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

        _certificacionRepository.DetachAllProperties();
        _certificacionEdit = null;
        CloseDialogWithResult(window, null);
    }

    private void InitializeCertificacion(Model.Certificacion certificacion)
    {
        CertificacionWrapper = new CertificacionWrapper(certificacion);
        CertificacionWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _certificacionRepository.HasChanges();

            if (e.PropertyName == nameof(CertificacionWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();
    }

    private Model.Certificacion CreateNewCertificacion()
    {
        var certificacion = new Model.Certificacion();
        certificacion.ResumeId = ResumeId;
        _certificacionRepository.Add(certificacion);
        return certificacion;
    }


    private async void OnNavigateToEditCertificacion(int certificacicacionId)
    {
        _certificacionEdit = await _certificacionRepository.Get(certificacicacionId);
    }

    #endregion
}