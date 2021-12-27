using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Habilidad;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Extensions;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class IdiomasHabilidadesSoftwareViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IHabilidadRepository _habilidadRepository;
    private readonly IDialogService _dialogService;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly HabilidadDialogViewModel _habilidadDialogViewModel;
    private readonly IEventAggregator _eventAggregator;

    #endregion

    #region Properties

    public string Name { get; set; } = "Idiomas/Habilidades/Software";

    private ObservableCollection<Habilidad> _habilidades;
    public ObservableCollection<Habilidad> Habilidades
    {
        get => _habilidades;
        set 
        {
            _habilidades = value;
            OnPropertyChanged(nameof(Habilidades));
        }
    }

    private Habilidad _habilidad;
    public Habilidad Habilidad
    {
        get => _habilidad;
        set 
        {
            _habilidad = value;
            OnPropertyChanged(nameof(Habilidad));
            DeleteHabilidadCommand.RaiseCanExecuteChanged();
            EditHabilidadCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand AddHabilidadCommand { get; private set; }
    public DelegateCommand EditHabilidadCommand { get; private set; }
    public DelegateCommand DeleteHabilidadCommand { get; private set; }

    #endregion

    #region Constructor

    public IdiomasHabilidadesSoftwareViewModel(IHabilidadRepository habilidadRepository,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel,
        AlertDialogViewModel alertDialogViewModel,
        HabilidadDialogViewModel habilidadDialogViewModel,
        IEventAggregator eventAggregator)
    {
        _habilidadRepository = habilidadRepository;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;
        _alertDialogViewModel = alertDialogViewModel;
        _habilidadDialogViewModel = habilidadDialogViewModel;
        _eventAggregator = eventAggregator;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddHabilidadCommand = new DelegateCommand(OnAddHabilidad);
        EditHabilidadCommand = new DelegateCommand(OnEditHabilidad, CanEditHabilidad);
        DeleteHabilidadCommand = new DelegateCommand(OnDeleteHabilidad, CanDeleteHabilidad);
    }

    #endregion

    #region Methods

    private bool CanDeleteHabilidad()
    {
        return Habilidad is not null;
    }

    private async void OnDeleteHabilidad()
    {
        if (Habilidad is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el idioma: {Habilidad.Nombre}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _habilidadRepository.Remove(Habilidad);
                Habilidades.Remove(Habilidad);

                await _habilidadRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private void OnNavigateToEditResume(int id)
    {
        ResumeId = id;
        Habilidades = _habilidadRepository.Find(x => x.ResumeId == id).OrderBy(x=> x.Tipo).ToObservableCollection();
    }

    private void OnAddHabilidad()
    {
        _habilidadDialogViewModel.ResumeId = ResumeId;
        var habilidad = _dialogService.OpenDialog(_habilidadDialogViewModel);

        if (habilidad is not null)
        {
            Habilidades.Add(habilidad);
        }
    }

    private bool CanEditHabilidad()
    {
        return Habilidad is not null;
    }

    private void OnEditHabilidad()
    {
        _eventAggregator.GetEvent<NavigateToEditHabilidad>().Publish(Habilidad);

        var habilidad = _dialogService.OpenDialog(_habilidadDialogViewModel);

        if (habilidad is not null)
        {
            //Workaround
            Habilidades.Remove(habilidad);
            Habilidades.Add(habilidad);
        }
    }
    #endregion
}