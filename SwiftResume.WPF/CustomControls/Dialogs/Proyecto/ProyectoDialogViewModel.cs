using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Proyecto;

public class ProyectoDialogViewModel : DialogViewModelBase<Model.Proyecto>
{
    #region Fields

    private readonly IProyectoRepository _proyectoRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private Model.Proyecto _proyectoEdit;

    #endregion

    #region Properties

    public int ResumeId { get; set; }

    private ProyectoWrapper _proyectoWrapper;
    public ProyectoWrapper ProyectoWrapper
    {
        get => _proyectoWrapper;
        set
        {
            _proyectoWrapper = value;
            OnPropertyChanged(nameof(ProyectoWrapper));
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

    public ProyectoDialogViewModel(IProyectoRepository proyectoRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _proyectoRepository = proyectoRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditProyecto>()
            .Subscribe(OnNavigateToEditProyecto);

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
    }


    #endregion

    #region Methods

    private void OnCancel(IDialogWindow window)
    {
        if (HasChanges)
        {
            _yesNoDialogViewModel.Message = $"Hay cambios pendientes, al cerrar la ventana se borrarán los cambios, ¿Desea cerrar la ventana?";
            var dialog = _dialogService.OpenDialog(_yesNoDialogViewModel);
            if (dialog == DialogResults.No)
                return;
        }

        _proyectoRepository.DetachAllProperties();
        _proyectoEdit = null;
        CloseDialogWithResult(window, null);
    }

    private bool CanSave(IDialogWindow arg)
    {
        return ProyectoWrapper != null && !ProyectoWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        ProyectoWrapper.Validate();
        if (!ProyectoWrapper.HasErrors)
        {
            _proyectoRepository.SaveAsync();
            HasChanges = _proyectoRepository.HasChanges();
            CloseDialogWithResult(window, ProyectoWrapper.Model);
        }
    }

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;

        var proyecto = (_proyectoEdit is not null) ?
            _proyectoEdit
            : CreateNewProyecto();

        InitializeProyecto(proyecto);
    }

    private void InitializeProyecto(Model.Proyecto proyecto)
    {
        ProyectoWrapper = new ProyectoWrapper(proyecto);
        ProyectoWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _proyectoRepository.HasChanges();

            if (e.PropertyName == nameof(ProyectoWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();
    }

    private Model.Proyecto CreateNewProyecto()
    {
        var proyecto = new Model.Proyecto();
        proyecto.ResumeId = ResumeId;
        _proyectoRepository.Add(proyecto);
        return proyecto;
    }

    private async void OnNavigateToEditProyecto(int proyectoId)
    {
        _proyectoEdit = await _proyectoRepository.Get(proyectoId);
    }
    #endregion
}