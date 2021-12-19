using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Idioma
{
    public class IdiomaDialogViewModel : DialogViewModelBase<Model.Idioma>
    {
        #region Fields

        private readonly IIdiomaRepository _idiomaRepository;
        private readonly IDialogService _dialogService;
        private readonly YesNoDialogViewModel _yesNoDialogViewModel;

        #endregion

        #region Properties

        private IdiomaWrapper _idiomaWrapper;
        public IdiomaWrapper IdiomaWrapper
        {
            get => _idiomaWrapper;
            set
            {
                _idiomaWrapper = value;
                OnPropertyChanged(nameof(IdiomaWrapper));
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

        public IdiomaDialogViewModel(IIdiomaRepository idiomaRepository,
            IDialogService dialogService,
            YesNoDialogViewModel yesNoDialogViewModel)
        {
            _idiomaRepository = idiomaRepository;
            _dialogService = dialogService;
            _yesNoDialogViewModel = yesNoDialogViewModel;

            SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
            CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        }
        #endregion

        #region Methods

        public void OnLoad()
        {
            //Restore has changes to false
            HasChanges = false;
            _idiomaRepository.DetachAllProperties();

            var idioma = CreateNewIdioma();

            InitializeResume(idioma);
        }

        private bool CanSave(IDialogWindow arg)
        {
            return IdiomaWrapper != null && !IdiomaWrapper.HasErrors && HasChanges;
        }

        private void OnSave(IDialogWindow window)
        {
            IdiomaWrapper.Validate();
            if (!IdiomaWrapper.HasErrors)
            {
                _idiomaRepository.SaveAsync();
                HasChanges = _idiomaRepository.HasChanges();
                CloseDialogWithResult(window, IdiomaWrapper.Model);
            }
        }

        private void OnCancel(IDialogWindow window)
        {
            if (HasChanges)
            {
                _yesNoDialogViewModel.Message = $"Hay cambios pendientes, al cerrar la ventana se borrarán los cambios, ¿Desea cerrar la ventana?";
                var dialog = _dialogService.OpenDialog(_yesNoDialogViewModel);
                if (dialog == DialogResults.No)
                    return;
            }
            CloseDialogWithResult(window, null);
        }

        private void InitializeResume(Model.Idioma idioma)
        {
            IdiomaWrapper = new IdiomaWrapper(idioma);
            IdiomaWrapper.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                    HasChanges = _idiomaRepository.HasChanges();

                if (e.PropertyName == nameof(IdiomaWrapper.HasErrors))
                    SaveCommand.RaiseCanExecuteChanged();
            };

            SaveCommand.RaiseCanExecuteChanged();
        }

        private Model.Idioma CreateNewIdioma()
        {
            var idioma = new Model.Idioma();
            _idiomaRepository.Add(idioma);
            return idioma;
        }

        #endregion
    }
}
