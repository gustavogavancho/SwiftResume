using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Habilidad
{
    public class HabilidadDialogViewModel : DialogViewModelBase<Model.Habilidad>
    {
        #region Fields

        private readonly IHabilidadRepository _habilidadRepository;
        private readonly IDialogService _dialogService;
        private readonly YesNoDialogViewModel _yesNoDialogViewModel;
        private Model.Habilidad _habilidadEdit;

        #endregion

        #region Properties

        public int ResumeId { get; set; }


        private HabilidadWrapper _habilidadWrapper;
        public HabilidadWrapper HabilidadWrapper
        {
            get => _habilidadWrapper;
            set
            {
                _habilidadWrapper = value;
                OnPropertyChanged(nameof(HabilidadWrapper));
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

        public HabilidadDialogViewModel(IHabilidadRepository habilidadRepository,
            IDialogService dialogService,
            YesNoDialogViewModel yesNoDialogViewModel,
            IEventAggregator eventAggregator)
        {
            _habilidadRepository = habilidadRepository;
            _dialogService = dialogService;
            _yesNoDialogViewModel = yesNoDialogViewModel;

            eventAggregator.GetEvent<NavigateToEditHabilidad>()
                .Subscribe(OnNavigateToEditHabilidad);


            SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
            CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        }

        #endregion

        #region Methods

        public void OnLoad()
        {
            //Restore has changes to false
            HasChanges = false;

            var idioma = (_habilidadEdit is not null)?
                _habilidadEdit
                : CreateNewIdioma();

            InitializeResume(idioma);
        }

        private bool CanSave(IDialogWindow arg)
        {
            return HabilidadWrapper != null && !HabilidadWrapper.HasErrors && HasChanges;
        }

        private void OnSave(IDialogWindow window)
        {
            HabilidadWrapper.Validate();
            if (!HabilidadWrapper.HasErrors)
            {
                _habilidadRepository.SaveAsync();
                HasChanges = _habilidadRepository.HasChanges();
                CloseDialogWithResult(window, HabilidadWrapper.Model);
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

            _habilidadRepository.DetachAllProperties();
            _habilidadEdit = null;
            CloseDialogWithResult(window, null);
        }

        private void InitializeResume(Model.Habilidad habilidad)
        {
            HabilidadWrapper = new HabilidadWrapper(habilidad);
            HabilidadWrapper.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                    HasChanges = _habilidadRepository.HasChanges();

                if (e.PropertyName == nameof(HabilidadWrapper.HasErrors))
                    SaveCommand.RaiseCanExecuteChanged();
            };

            SaveCommand.RaiseCanExecuteChanged();
        }

        private Model.Habilidad CreateNewIdioma()
        {
            var habilidad = new Model.Habilidad();
            habilidad.ResumeId = ResumeId;
            _habilidadRepository.Add(habilidad);
            return habilidad;
        }

        private void OnNavigateToEditHabilidad(Model.Habilidad habilidad)
        {
            _habilidadEdit = habilidad;
        }
        #endregion
    }
}
