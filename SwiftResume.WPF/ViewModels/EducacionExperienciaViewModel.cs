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

    private Educacion _educacionSelected = new();
    public Educacion EducacionSelected
    {
        get => _educacionSelected;
        set 
        { 
            _educacionSelected = value;
            OnPropertyChanged(nameof(EducacionSelected));
            DeleteEducacionExperienciaCommand.RaiseCanExecuteChanged();
            EditEducacionExperienciaCommand.RaiseCanExecuteChanged();
        }
    }

    private Experiencia _experienciaSelected = new();
    public Experiencia ExperienciaSelected
    {
        get => _experienciaSelected;
        set
        {
            _experienciaSelected = value;
            OnPropertyChanged(nameof(ExperienciaSelected));
            DeleteEducacionExperienciaCommand.RaiseCanExecuteChanged();
            EditEducacionExperienciaCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand<object> AddEducacionExperienciaCommand { get; private set; }
    public DelegateCommand<object> EditEducacionExperienciaCommand { get; private set; }
    public DelegateCommand<object> DeleteEducacionExperienciaCommand { get; private set; }


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

        AddEducacionExperienciaCommand = new DelegateCommand<object>(OnAddEducacionExperiencia);
        EditEducacionExperienciaCommand = new DelegateCommand<object>(OnEditEducacionExperiencia, CanEditEducacionExperiencia);
        DeleteEducacionExperienciaCommand = new DelegateCommand<object>(OnDeleteEducacionExpericena, CanDeleteEducacionExperiencia);
    }

    #endregion

    #region Methods

    private void OnNavigateToEditResume(int id)
    {
        ResumeId = id;
        Educacion = _educacionRepository.Find(x => x.ResumeId == id).ToObservableCollection();
        Experiencia = _experienciaRepository.Find(x => x.ResumeId == id).ToObservableCollection();
    }


    private void OnAddEducacionExperiencia(object obj)
    {
        if (obj is Educacion)
        {
            _educacionDialogViewModel.ResumeId = ResumeId;

            var educacion = _dialogService.OpenDialog(_educacionDialogViewModel);

            if (educacion is not null)
            {
                Educacion.Add(educacion);
            }
        }
        else if (obj is Experiencia)
        {
            _experienciaDialogViewModel.ResumeId = ResumeId;

            var experiencia = _dialogService.OpenDialog(_experienciaDialogViewModel);

            if (experiencia is not null)
            {
                Experiencia.Add(experiencia);
            }
        }
    }

    private bool CanDeleteEducacionExperiencia(object obj)
    {
        return CanEditDeleteEducacionExperiencia(obj);
    }

    private async void OnDeleteEducacionExpericena(object obj)
    {
        if (obj is Educacion edu)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el grado académico: {edu.Descripcion} - {edu.Institucion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _educacionRepository.Remove(edu);
                Educacion.Remove(edu);

                await _educacionRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
        else if (obj is Experiencia exp)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el grado académico: {exp.Descripcion} - {exp.Institucion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _experienciaRepository.Remove(exp);
                Experiencia.Remove(exp);

                await _experienciaRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private bool CanEditEducacionExperiencia(object obj)
    {
        return CanEditDeleteEducacionExperiencia(obj);
    }

    private void OnEditEducacionExperiencia(object obj)
    {
        if (obj is Educacion edu)
        {
            _eventAggregator.GetEvent<NavigateToEditEducacion>().Publish(edu.Id);

            var habilidad = _dialogService.OpenDialog(_educacionDialogViewModel);

            if (habilidad is not null)
            {
                //Workaround
                Educacion.Remove(habilidad);
                Educacion.Add(habilidad);
            }
        }
        else if (obj is Experiencia exp)
        {
            _eventAggregator.GetEvent<NavigateToEditExperiencia>().Publish(exp.Id);

            var experiencia = _dialogService.OpenDialog(_experienciaDialogViewModel);

            if (experiencia is not null)
            {
                //Workaround
                Experiencia.Remove(experiencia);
                Experiencia.Add(experiencia);
            }
        }
    }

    private bool CanEditDeleteEducacionExperiencia(object obj)
    {
        if (obj is Educacion)
        {
            return Educacion is not null;
        }
        else if (obj is Experiencia)
        {
            return Experiencia is not null;
        }
        else return false;
    }

    #endregion
}