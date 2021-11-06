using Prism.Commands;
using SwiftResume.BIZ.Repositories;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
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

        #endregion

        #region Commands

        public DelegateCommand<IDialogWindow> SaveCommand { get; private set; }
        public DelegateCommand<IDialogWindow> CancelCommand { get; private set; }

        #endregion

        #region Constructor

        public ResumeDialogViewModel(IResumeRepository resumeRepository,
            IUserStored userStored) : base()
        {
            _resumeRepository = resumeRepository;
            _userStored = userStored;
            SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
            CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        }

        #endregion

        #region Methods

        private bool CanSave(IDialogWindow arg)
        {
            return ResumeWrapper != null && !ResumeWrapper.HasErrors;
        }

        private void OnSave(IDialogWindow window)
        {
            ResumeWrapper.Model.Username = _userStored.CurrentUser.Username;
            _resumeRepository.Add(ResumeWrapper.Model);
            CloseDialogWithResult(window, ResumeWrapper.Model);
        }

        private void OnCancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, null);
        }

        public void OnLoad()
        {
            ResumeWrapper = new ResumeWrapper(new Model.Resume());
            ResumeWrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ResumeWrapper.HasErrors))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            };

            SaveCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
