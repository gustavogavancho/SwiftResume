using Prism.Commands;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using System;
using System.Collections.ObjectModel;
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

            var getRepository = _resumeRepository.GetAll();

            //TODO: Delete mock resume data
            Resumes = new ObservableCollection<Resume>
            {
                new Resume
                {
                    Id = 1,
                    Nombres = "Gustavo",
                    Apellidos = "Gavancho León",
                    Genero = "Masculino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.Español),
                },
                new Resume
                {
                    Id = 2,
                    Nombres = "Jordi",
                    Apellidos = "Gavancho León",
                    Genero = "Masculino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.English)
                },
                new Resume
                {
                    Id = 3,
                    Nombres = "Milagros",
                    Apellidos = "Iñipe Cachay",
                    Genero = "Femenino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.English)
                },
                new Resume
                {
                    Id = 4,
                    Nombres = "Olga Cristina del Rocio",
                    Apellidos = "Gavancho León",
                    Genero = "Femenino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.Español)
                },
                new Resume
                {
                    Id = 5,
                    Nombres = "Olga del Rocio",
                    Apellidos = "León García",
                    Genero = "Femenino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.Español)
                },
                new Resume
                {
                    Id = 6,
                    Nombres = "Gustavo Efrain",
                    Apellidos = "Gavancho Pedreschi",
                    Genero = "Masculino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.Español)
                },
                new Resume
                {
                    Id = 7,
                    Nombres = "Toty Ernestina",
                    Apellidos = "Córdova Rios",
                    Genero = "Feminino",
                    Lenguaje = Enum.GetName(EnumLanguage.Language.Español)
                }
            };

            DeleteCommand = new DelegateCommand(OnDelete);
        }

        private void OnDelete()
        {
            if (Resume != null)
            {
                var dialog = new YesNoDialogViewModel($"¿Deseal eliminar el curriculum de {Resume.Nombres} {Resume.Apellidos}?");
    
                var result = _dialogService.OpenDialog(dialog);

                if (result == DialogResults.Si)
                {
                    Resumes.Remove(Resume);
                }
            }
        }

        #endregion

    }
}
