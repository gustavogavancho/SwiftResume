using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Certificacion;
using SwiftResume.WPF.CustomControls.Dialogs.Proyecto;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Extensions;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class CertificacionProyectoViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly ICertificacionRepository _certificacionRepository;
    private readonly IProyectoRepository _proyectoRepository;
    private readonly IDialogService _dialogService;
    private readonly CertificacionDialogViewModel _certificacionDialogViewModel;
    private readonly ProyectoDialogViewModel _proyectoDialogViewModel;
    private readonly IEventAggregator _eventAggregator;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;

    #endregion

    #region Properties

    public string Name { get; set; } = "Certificaciones/Proyectos";

    private ObservableCollection<Certificacion> _certificaciones = new();
    public ObservableCollection<Certificacion> Certificaciones
    {
        get => _certificaciones;
        set
        {
            _certificaciones = value;
            OnPropertyChanged(nameof(Certificaciones));
        }
    }

    private ObservableCollection<Proyecto> _proyectos = new();
    public ObservableCollection<Proyecto> Proyectos
    {
        get => _proyectos;
        set
        {
            _proyectos = value;
            OnPropertyChanged(nameof(Proyectos));
        }
    }

    private Certificacion _certificacionSelected;
    public Certificacion CertificacionSelected
    {
        get => _certificacionSelected;
        set
        {
            _certificacionSelected = value;
            OnPropertyChanged(nameof(CertificacionSelected));
            EditCertificacionCommand.RaiseCanExecuteChanged();
            DeleteCertificacionCommand.RaiseCanExecuteChanged();
        }
    }

    private Proyecto _proyectoSelected;
    public Proyecto ProyectoSelected
    {
        get => _proyectoSelected;
        set
        {
            _proyectoSelected = value;
            OnPropertyChanged(nameof(ProyectoSelected));
            EditProyectoCommand.RaiseCanExecuteChanged();
            DeleteProyectoCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand AddCertificacionCommand { get; private set; }
    public DelegateCommand EditCertificacionCommand { get; private set; }
    public DelegateCommand DeleteCertificacionCommand { get; private set; }

    public DelegateCommand AddProyectoCommand { get; private set; }
    public DelegateCommand EditProyectoCommand { get; private set; }
    public DelegateCommand DeleteProyectoCommand { get; private set; }

    #endregion

    #region Constructor

    public CertificacionProyectoViewModel(ICertificacionRepository certificacionRepository,
        IProyectoRepository proyectoRepository,
        IDialogService dialogService,
        CertificacionDialogViewModel certificacionDialogViewModel,
        ProyectoDialogViewModel proyectoDialogViewModel,
        IEventAggregator eventAggregator,
        AlertDialogViewModel alertDialogViewModel,
        YesNoDialogViewModel yesNoDialogViewModel)
    {
        _certificacionRepository = certificacionRepository;
        _proyectoRepository = proyectoRepository;
        _dialogService = dialogService;
        _certificacionDialogViewModel = certificacionDialogViewModel;
        _proyectoDialogViewModel = proyectoDialogViewModel;
        _eventAggregator = eventAggregator;
        _alertDialogViewModel = alertDialogViewModel;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddCertificacionCommand = new DelegateCommand(OnAddCertificacion);
        EditCertificacionCommand = new DelegateCommand(OnEditCertificacion, CanEditDeleteCertificacion);
        DeleteCertificacionCommand = new DelegateCommand(OnDeleteCertificacion, CanEditDeleteCertificacion);
        AddProyectoCommand = new DelegateCommand(OnAddProyecto);
        EditProyectoCommand = new DelegateCommand(OnEditProyecto, CanEditDeleteProyecto);
        DeleteProyectoCommand = new DelegateCommand(OnDeleteProyecto, CanEditDeleteProyecto);
    }

    #endregion

    #region Methods
    private async void OnDeleteProyecto()
    {
        if (ProyectoSelected is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el certificado: {ProyectoSelected.Descripcion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _proyectoRepository.Remove(ProyectoSelected);
                Proyectos.Remove(ProyectoSelected);

                await _proyectoRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private bool CanEditDeleteProyecto()
    {
        return ProyectoSelected is not null;
    }

    private void OnEditProyecto()
    {
        if (ProyectoSelected is not null)
        {
            _eventAggregator.GetEvent<NavigateToEditProyecto>().Publish(ProyectoSelected.Id);

            var proyecto = _dialogService.OpenDialog(_proyectoDialogViewModel);

            if (proyecto is not null)
            {
                //Workaround
                Proyectos = _proyectoRepository.Find(x => x.ResumeId == ResumeId)
                    .ToObservableCollection();
            }
        }
    }

    private bool CanEditDeleteCertificacion()
    {
        return CertificacionSelected is not null;
    }

    private void OnEditCertificacion()
    {
        if (CertificacionSelected is not null)
        {
            _eventAggregator.GetEvent<NavigateToEditCertificacion>().Publish(CertificacionSelected.Id);

            var certificacion = _dialogService.OpenDialog(_certificacionDialogViewModel);

            if (certificacion is not null)
            {
                //Workaround
                Certificaciones = _certificacionRepository.Find(x => x.ResumeId == ResumeId)
                    .OrderByDescending(x => x.Fecha)
                    .ToObservableCollection();
            }
        }
    }

    private async void OnDeleteCertificacion()
    {
        if (CertificacionSelected is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el certificado: {CertificacionSelected.Descripcion} - {CertificacionSelected.Institucion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _certificacionRepository.Remove(CertificacionSelected);
                Certificaciones.Remove(CertificacionSelected);

                await _certificacionRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private void OnNavigateToEditResume(int id)
    {
        ResumeId = id;

        Certificaciones = _certificacionRepository.Find(x => x.ResumeId == id)
            .OrderByDescending(x=> x.Fecha)
            .ToObservableCollection();

        Proyectos = _proyectoRepository.Find(x => x.ResumeId == id)
            .OrderBy(x=> x.TipoProyecto)
            .ToObservableCollection();
    }

    private void OnAddCertificacion()
    {
        _certificacionDialogViewModel.ResumeId = ResumeId;

        var certificacion = _dialogService.OpenDialog(_certificacionDialogViewModel);

        if (certificacion is not null)
        {
            Certificaciones.Add(certificacion);
        }
    }

    private void OnAddProyecto()
    {
        _proyectoDialogViewModel.ResumeId = ResumeId;

        var proyecto = _dialogService.OpenDialog(_proyectoDialogViewModel);

        if (proyecto is not null)
        {
            Proyectos.Add(proyecto);
        }
    }

    #endregion

}