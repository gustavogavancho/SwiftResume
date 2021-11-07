using Prism.Commands;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.State.Users;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Resume
{
    public class ResumeDialogViewModel : DialogViewModelBase<Model.Resume>
    {
        #region Fields

        private readonly IResumeRepository _resumeRepository;
        private readonly IUserStored _userStored;
        private readonly IDialogService _dialogService;
        private readonly YesNoDialogViewModel _yesNoDialogViewModel;

        #endregion

        #region Properties

        private ResumeWrapper _resumeWrapper;
        public ResumeWrapper ResumeWrapper
        {
            get => _resumeWrapper;
            set 
            {
                _resumeWrapper = value;
                OnPropertyChanged(nameof(ResumeWrapper));
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

        public ResumeDialogViewModel(IResumeRepository resumeRepository,
            IUserStored userStored,
            IDialogService dialogService,
            YesNoDialogViewModel yesNoDialogViewModel) : base()
        {
            _resumeRepository = resumeRepository;
            _userStored = userStored;
            _dialogService = dialogService;
            _yesNoDialogViewModel = yesNoDialogViewModel;
            SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
            CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        }

        #endregion

        #region Methods

        private bool CanSave(IDialogWindow arg)
        {
            return ResumeWrapper != null && !ResumeWrapper.HasErrors && HasChanges;
        }

        private void OnSave(IDialogWindow window)
        {
            ResumeWrapper.Model.Username = _userStored.CurrentUser.Username;
            _resumeRepository.Add(ResumeWrapper.Model);
            _resumeRepository.SaveAsync();
            HasChanges = _resumeRepository.HasChanges();
            CloseDialogWithResult(window, ResumeWrapper.Model);
        }

        private void OnCancel(IDialogWindow window)
        {
            if (HasChanges)
            {
                _yesNoDialogViewModel.Message = $"Hay cambios pendientes, al cerrar la ventana se borrarán los cambios, ¿Desea cerrar la ventana?";
                var dialog = _dialogService.OpenDialog(_yesNoDialogViewModel);
                if (dialog == DialogResults.No)
                {
                    return;
                }
            }
            CloseDialogWithResult(window, null);
        }

        public void OnLoad()
        {
            var resume = CreateNewResume();

            InitializeResume(resume);
        }

        private void InitializeResume(Model.Resume resume)
        {
            ResumeWrapper = new ResumeWrapper(resume);
            ResumeWrapper.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                    HasChanges = _resumeRepository.HasChanges();

                if (e.PropertyName == nameof(ResumeWrapper.HasErrors))
                    SaveCommand.RaiseCanExecuteChanged();
            };

            SaveCommand.RaiseCanExecuteChanged();
        }

        private Model.Resume CreateNewResume()
        {
            var friend = new Model.Resume();
            _resumeRepository.Add(friend);
            return friend;
        }

        #endregion
    }
}
