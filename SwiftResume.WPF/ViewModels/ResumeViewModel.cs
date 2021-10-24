using Prism.Commands;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EnumLanguage = SwiftResume.COMMON.Enums;

namespace SwiftResume.WPF.ViewModels
{
    public class ResumeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDialogService _dialogService;
        private readonly IResumeRepository _resumeRepository;

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

        private int _index;
        public int Index
        {
            get => _index;
            set 
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        #endregion

        #region Commands

        public DelegateCommand DeleteCommand { get; private set; }

        #endregion

        #region Constructor

        public ResumeViewModel(IDialogService dialogService,
            IResumeRepository resumeRepository)
        {
            _dialogService = dialogService;
            _resumeRepository = resumeRepository;

            DeleteCommand = new DelegateCommand(OnDelete);
        }

        private async void OnDelete()
        {
            if (Resume != null)
            {
                var dialog = new YesNoDialogViewModel($"¿Deseal eliminar el curriculum de {Resume.Nombres} {Resume.Apellidos}?");
    
                var result = _dialogService.OpenDialog(dialog);

                if (result == DialogResults.Si)
                {
                    await _resumeRepository.Remove(Resume);
                    Resumes.Remove(Resume);

                    var alert = new AlertDialogViewModel("Se eliminó correctamente el curriculum");

                    _dialogService.OpenDialog(alert);
                }
            }
        }

        public async void OnLoad()
        {
            var resume = await _resumeRepository.GetAll();
            Resumes = resume.ToObservableCollection();
        }

        #endregion

    }
}
