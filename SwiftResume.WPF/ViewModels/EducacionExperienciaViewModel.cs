using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Educacion;
using SwiftResume.WPF.CustomControls.Dialogs.Experiencia;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Extensions;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class EducacionExperienciaViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IExperienciaRepository _experienciaRepository;
    private readonly IEducacionRepository _educacionRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly IDialogService _dialogService;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private readonly EducacionDialogViewModel _educacionDialogViewModel;
    private readonly ExperienciaDialogViewModel _experienciaDialogViewModel;

    #endregion

    #region Properties

    public string Name { get; set; } = "Educación/Experiencia";

    private ObservableCollection<Educacion> _educacion;
    public ObservableCollection<Educacion> Educacion
    {
        get => _educacion;
        set 
        {
            _educacion = value;
            OnPropertyChanged(nameof(Educacion));
        }
    }

    private ObservableCollection<Experiencia> _experiencia;
    public ObservableCollection<Experiencia> Experiencia
    {
        get => _experiencia;
        set
        {
            _experiencia = value;
            OnPropertyChanged(nameof(Experiencia));
        }
    }

    private Educacion _educacionSelected;
    public Educacion EducacionSelected
    {
        get => _educacionSelected;
        set 
        { 
            _educacionSelected = value;
            OnPropertyChanged(nameof(EducacionSelected));
            DeleteEducacionCommand.RaiseCanExecuteChanged();
            EditEducacionCommand.RaiseCanExecuteChanged();
        }
    }

    private Experiencia _experienciaSelected;
    public Experiencia ExperienciaSelected
    {
        get => _experienciaSelected;
        set
        {
            _experienciaSelected = value;
            OnPropertyChanged(nameof(ExperienciaSelected));
            DeleteExperienciaCommand.RaiseCanExecuteChanged();
            EditExperienciaCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand AddEducacionCommand { get; private set; }
    public DelegateCommand AddExperienciaCommand { get; private set; }
    public DelegateCommand EditEducacionCommand { get; private set; }
    public DelegateCommand EditExperienciaCommand { get; private set; }
    public DelegateCommand DeleteEducacionCommand { get; private set; }
    public DelegateCommand DeleteExperienciaCommand { get; private set; }

    #endregion

    #region Constructor

    public EducacionExperienciaViewModel(IExperienciaRepository experienciaRepository,
        IEducacionRepository educacionRepository,
        IEventAggregator eventAggregator,
        IDialogService dialogService,
        AlertDialogViewModel alertDialogViewModel,
        YesNoDialogViewModel yesNoDialogViewModel,
        EducacionDialogViewModel educacionDialogViewModel,
        ExperienciaDialogViewModel experienciaDialogViewModel)
    {
        _experienciaRepository = experienciaRepository;
        _educacionRepository = educacionRepository;
        _eventAggregator = eventAggregator;
        _dialogService = dialogService;
        _alertDialogViewModel = alertDialogViewModel;
        _yesNoDialogViewModel = yesNoDialogViewModel;
        _educacionDialogViewModel = educacionDialogViewModel;
        _experienciaDialogViewModel = experienciaDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddEducacionCommand = new DelegateCommand(OnAddEducacion);
        AddExperienciaCommand = new DelegateCommand(OnAddExperiencia);
        EditEducacionCommand = new DelegateCommand(OnEditEducacion, CanEditDeleteEducacion);
        EditExperienciaCommand = new DelegateCommand(OnEditExperiencia, CanEditDeleteExperiencia);
        DeleteEducacionCommand = new DelegateCommand(OnDeleteEducacion, CanEditDeleteEducacion);
        DeleteExperienciaCommand = new DelegateCommand(OnDeleteExperiencia, CanEditDeleteExperiencia);
    }

    #endregion

    #region Methods

    private bool CanEditDeleteEducacion()
    {
        return EducacionSelected is not null;
    }

    private bool CanEditDeleteExperiencia()
    {
        return ExperienciaSelected is not null;
    }

    private void OnNavigateToEditResume(int id)
    {
        ResumeId = id;

        Educacion = _educacionRepository.Find(x => x.ResumeId == id)
            .OrderByDescending(x=> x.FechaFin)
            .ToObservableCollection();
        Experiencia = _experienciaRepository.Find(x => x.ResumeId == id)
            .OrderByDescending(x=> x.FechaInicio)
            .ToObservableCollection();
    }

    private void OnAddEducacion()
    {
        _educacionDialogViewModel.ResumeId = ResumeId;

        var educacion = _dialogService.OpenDialog(_educacionDialogViewModel);

        if (educacion is not null)
        {
            Educacion.Add(educacion);
        }
    }

    private void OnAddExperiencia()
    {
        _experienciaDialogViewModel.ResumeId = ResumeId;

        var experiencia = _dialogService.OpenDialog(_experienciaDialogViewModel);

        if (experiencia is not null)
        {
            Experiencia.Add(experiencia);
        }
    }

    private async void OnDeleteEducacion()
    {
        if (EducacionSelected is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el grado académico: {EducacionSelected.Descripcion} - {EducacionSelected.Institucion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _educacionRepository.Remove(EducacionSelected);
                Educacion.Remove(EducacionSelected);

                await _educacionRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private async void OnDeleteExperiencia()
    {
        if (ExperienciaSelected is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el grado académico: {ExperienciaSelected.Descripcion} - {ExperienciaSelected.Institucion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _experienciaRepository.Remove(ExperienciaSelected);
                Experiencia.Remove(ExperienciaSelected);

                await _experienciaRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private void OnEditEducacion()
    {
        if (EducacionSelected is not null)
        {
            _eventAggregator.GetEvent<NavigateToEditEducacion>().Publish(EducacionSelected.Id);

            var habilidad = _dialogService.OpenDialog(_educacionDialogViewModel);

            if (habilidad is not null)
            {
                //Workaround
                Educacion.Remove(habilidad);
                Educacion.Add(habilidad);
            }
        }
    }

    private void OnEditExperiencia()
    {
        if (ExperienciaSelected is not null)
        {
            _eventAggregator.GetEvent<NavigateToEditExperiencia>().Publish(ExperienciaSelected.Id);

            var experiencia = _dialogService.OpenDialog(_experienciaDialogViewModel);

            if (experiencia is not null)
            {
                //Workaround
                Experiencia.Remove(experiencia);
                Experiencia.Add(experiencia);
            }
        }
    }

    #endregion
}