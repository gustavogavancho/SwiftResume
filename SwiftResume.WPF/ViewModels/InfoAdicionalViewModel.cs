using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.InfoAdicional;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Extensions;
using System.Collections.ObjectModel;

namespace SwiftResume.WPF.ViewModels;

public class InfoAdicionalViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IInfoAdicionalRepository _infoAdicionalRepository;
    private readonly InfoAdicionalDialogViewModel _infoAdicionalDialogViewModel;
    private readonly IDialogService _dialogService;
    private readonly IEventAggregator _eventAggregator;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;

    #endregion

    #region Properties

    public string Name { get; set; } = "Info Adicional";

    private ObservableCollection<InfoAdicional> _infoAdicional = new();
    public ObservableCollection<InfoAdicional> InfoAdicional
    {
        get => _infoAdicional;
        set
        {
            _infoAdicional = value;
            OnPropertyChanged(nameof(InfoAdicional));
        }
    }

    private InfoAdicional _infoAdicioanlSelected;
    public InfoAdicional InfoAdicionalSelected
    {
        get => _infoAdicioanlSelected;
        set
        {
            _infoAdicioanlSelected = value;
            OnPropertyChanged(nameof(InfoAdicionalSelected));
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand AddCommand { get; private set; }
    public DelegateCommand EditCommand { get; private set; }
    public DelegateCommand DeleteCommand { get; private set; }


    #endregion

    #region Constructor

    public InfoAdicionalViewModel(IInfoAdicionalRepository infoAdicionalRepository,
        InfoAdicionalDialogViewModel infoAdicionalDialogViewModel,
        IDialogService dialogService,
        IEventAggregator eventAggregator,
        AlertDialogViewModel alertDialogViewModel,
        YesNoDialogViewModel yesNoDialogViewModel)
    {
        _infoAdicionalRepository = infoAdicionalRepository;
        _infoAdicionalDialogViewModel = infoAdicionalDialogViewModel;
        _dialogService = dialogService;
        _eventAggregator = eventAggregator;
        _alertDialogViewModel = alertDialogViewModel;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        AddCommand = new DelegateCommand(OnAdd);
        EditCommand = new DelegateCommand(OnEdit, CanEditDelete);
        DeleteCommand = new DelegateCommand(OnDelete, CanEditDelete);

    }

    #endregion

    #region Methods

    private async void OnDelete()
    {
        if (InfoAdicionalSelected is not null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el certificado: {InfoAdicionalSelected.Descripcion}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _infoAdicionalRepository.Remove(InfoAdicionalSelected);
                InfoAdicional.Remove(InfoAdicionalSelected);

                await _infoAdicionalRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    private bool CanEditDelete()
    {
        return InfoAdicionalSelected is not null;
    }

    private void OnEdit()
    {
        if (InfoAdicionalSelected is not null)
        {
            _eventAggregator.GetEvent<NavigateToEditInfoAdicional>().Publish(InfoAdicionalSelected.Id);

            var infoAdicional = _dialogService.OpenDialog(_infoAdicionalDialogViewModel);

            if (infoAdicional is not null)
            {
                //Workaround
                InfoAdicional = _infoAdicionalRepository.Find(x => x.ResumeId == ResumeId)
                    .ToObservableCollection();
            }
        }
    }

    private void OnAdd()
    {
        _infoAdicionalDialogViewModel.ResumeId = ResumeId;

        var infoAdicional = _dialogService.OpenDialog(_infoAdicionalDialogViewModel);

        if (infoAdicional is not null)
        {
            InfoAdicional.Add(infoAdicional);
        }
    }


    private void OnNavigateToEditResume(int id)
    {
        ResumeId = id;

        InfoAdicional = _infoAdicionalRepository.Find(x => x.ResumeId == id)
            .ToObservableCollection();
    }

    #endregion
}