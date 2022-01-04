using Microsoft.Win32;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.State.Users;
using SwiftResume.WPF.Wrapper;
using System.IO;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Resume;

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
    public DelegateCommand AgregarFotoCommand { get; private set; }

    #endregion

    #region Constructor

    public ResumeDialogViewModel(IResumeRepository resumeRepository,
        IUserStored userStored,
        IDialogService dialogService,
        YesNoDialogViewModel yesNoDialogViewModel)
    {
        _resumeRepository = resumeRepository;
        _userStored = userStored;
        _dialogService = dialogService;
        _yesNoDialogViewModel = yesNoDialogViewModel;

        SaveCommand = new DelegateCommand<IDialogWindow>(OnSave, CanSave);
        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        AgregarFotoCommand = new DelegateCommand(OnAgregarFoto);
    }

    #endregion

    #region Methods
    private void OnAgregarFoto()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            string var = ImageToBase64(openFileDialog.FileName);

            ResumeWrapper.FotoString = ResumeWrapper.Model.FotoString = var;
        }
    }

    public static string ImageToBase64(string _imagePath)
    {
        string _base64String = null;

        using (System.Drawing.Image _image = System.Drawing.Image.FromFile(_imagePath))
        {
            using (MemoryStream _mStream = new MemoryStream())
            {
                _image.Save(_mStream, _image.RawFormat);
                byte[] _imageBytes = _mStream.ToArray();
                _base64String = Convert.ToBase64String(_imageBytes);

                return _base64String;
            }
        }
    }

    public void OnLoad()
    {
        //Restore has changes to false
        HasChanges = false;
        _resumeRepository.DetachAllProperties();

        var resume = CreateNewResume();

        InitializeResume(resume);
    }

    private bool CanSave(IDialogWindow arg)
    {
        return ResumeWrapper != null && !ResumeWrapper.HasErrors && HasChanges;
    }

    private void OnSave(IDialogWindow window)
    {
        ResumeWrapper.Validate();
        if (!ResumeWrapper.HasErrors)
        {
            ResumeWrapper.Model.Username = _userStored.CurrentUser.Username;
            _resumeRepository.SaveAsync();
            HasChanges = _resumeRepository.HasChanges();
            CloseDialogWithResult(window, ResumeWrapper.Model);
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
