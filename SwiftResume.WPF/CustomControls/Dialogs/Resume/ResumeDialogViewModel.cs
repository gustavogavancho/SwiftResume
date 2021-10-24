using Prism.Commands;
using SwiftResume.BIZ.Repositories;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Resume
{
    public class ResumeDialogViewModel : DialogViewModelBase<Model.Resume>
    {
        #region Fields

        private readonly IResumeRepository _resumeRepository;

        #endregion

        #region Properties

        private Model.Resume _resume = new Model.Resume();

        public Model.Resume Resume
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

        public DelegateCommand<IDialogWindow> SaveCommand { get; private set; }
        public DelegateCommand<IDialogWindow> CancelCommand { get; private set; }

        #endregion

        #region Constructor

        public ResumeDialogViewModel(IResumeRepository resumeRepository) : base()
        {
            _resumeRepository = resumeRepository;

            SaveCommand = new DelegateCommand<IDialogWindow>(OnSave);
            CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        }

        #endregion

        #region Methods

        private void OnSave(IDialogWindow window)
        {
            _resumeRepository.Add(Resume);
            CloseDialogWithResult(window, Resume);
        }

        private void OnCancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, null);
        }

        public void OnLoad()
        {
            Resume = new Model.Resume();
        }

        #endregion
    }
}
