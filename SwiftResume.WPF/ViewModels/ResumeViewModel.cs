﻿using System.Collections.ObjectModel;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Resume;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Extensions;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.State.Users;

namespace SwiftResume.WPF.ViewModels;

public class ResumeViewModel : ViewModelBase
{
    #region Fields

    private readonly IDialogService _dialogService;
    private readonly IResumeRepository _resumeRepository;
    private readonly ResumeDialogViewModel _resumeDialogViewModel;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly IUserStored _userStored;
    private readonly ViewModelDelegateRenavigator<EditViewModel> _editNavigator;

    #endregion

    #region Properties

    private ObservableCollection<Resume> _resumes;
    public ObservableCollection<Resume> Resumes
    {
        get => _resumes;
        set
        {
            _resumes = value;
            OnPropertyChanged(nameof(Resumes));
        }
    }

    private Resume _resume;
    public Resume Resume
    {
        get => _resume;
        set
        {
            _resume = value;
            OnPropertyChanged(nameof(Resume));
        }
    }

    #endregion

    #region Commands

    public DelegateCommand AddCommand { get; private set; }
    public DelegateCommand EditCommand { get; private set; }
    public DelegateCommand DeleteCommand { get; private set; }

    #endregion

    #region Constructor

    public ResumeViewModel(IResumeRepository resumeRepository,
        IDialogService dialogService,
        ResumeDialogViewModel resumeDialogViewModel,
        YesNoDialogViewModel yesNoDialogViewModel,
        AlertDialogViewModel alertDialogViewModel,
        IUserStored userStored,
        ViewModelDelegateRenavigator<EditViewModel> editNavigator)
    {
        _resumeRepository = resumeRepository;
        _dialogService = dialogService;
        _resumeDialogViewModel = resumeDialogViewModel;
        _yesNoDialogViewModel = yesNoDialogViewModel;
        _alertDialogViewModel = alertDialogViewModel;
        _userStored = userStored;
        _editNavigator = editNavigator;

        AddCommand = new DelegateCommand(OnAdd);
        EditCommand = new DelegateCommand(OnEdit);
        DeleteCommand = new DelegateCommand(OnDelete);
    }

    #endregion

    #region Methods

    private void OnAdd()
    {
        var result = _dialogService.OpenDialog(_resumeDialogViewModel);

        if (result is not null)
        {
            Resumes.Add(result);
        }
    }

    private async void OnDelete()
    {
        if (Resume != null)
        {
            _yesNoDialogViewModel.Message = $"¿Deseal eliminar el curriculum de {Resume.Nombres} {Resume.Apellidos}?";

            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _resumeRepository.Remove(Resume);
                Resumes.Remove(Resume);

                await _resumeRepository.SaveAsync();

                _alertDialogViewModel.Message = "Se eliminó correctamente el registro.";

                _dialogService.OpenDialog(_alertDialogViewModel);
            }
        }
    }

    public async void OnLoad()
    {
        var resume = await _resumeRepository.GetResumesByUsername(_userStored.CurrentUser.Username);
        Resumes = resume.ToObservableCollection();
    }

    private void OnEdit()
    {
        _editNavigator.Renavigate();
    }


    #endregion
}