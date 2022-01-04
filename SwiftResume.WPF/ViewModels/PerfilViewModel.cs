using Microsoft.Win32;
using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Tab;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.Wrapper;
using System.IO;

namespace SwiftResume.WPF.ViewModels;

public class PerfilViewModel : ViewModelBase, ITab
{
    #region Fields

    private readonly IResumeRepository _resumeRepository;
    private readonly IDialogService _dialogService;
    private readonly AlertDialogViewModel _alertDialogViewModel;

    #endregion

    #region Properties

    public string Name { get; set; } = "Perfil";


    private Resume _resume;
    public Resume Resume
    {
        get => _resume;
        set
        {
            _resume = value;
            OnPropertyChanged(nameof(Resume));
            OnPropertyChanged(nameof(PerfilWrapper));
        }
    }

    private PerfilWrapper _perfilWrapper;
    public PerfilWrapper PerfilWrapper
    {
        get => _perfilWrapper;
        set
        {
            _perfilWrapper = value;
            OnPropertyChanged(nameof(PerfilWrapper));
        }
    }

    private bool _hasChanges;
    public bool HasChanges
    {
        get => _hasChanges;
        set
        {
            _hasChanges = value;
            OnPropertyChanged(nameof(HasChanges));
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    #endregion

    #region Commands

    public DelegateCommand SaveCommand { get; set; }
    public DelegateCommand AgregarFotoCommand { get; private set; }

    #endregion

    #region Constuctor
    public PerfilViewModel(IEventAggregator eventAggregator,
        IResumeRepository resumeRepository,
        IDialogService dialogService,
        AlertDialogViewModel alertDialogViewModel)
    {
        _resumeRepository = resumeRepository;
        _dialogService = dialogService;
        _alertDialogViewModel = alertDialogViewModel;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        SaveCommand = new DelegateCommand(OnSave, CanSave);
        AgregarFotoCommand = new DelegateCommand(OnAgregarFoto);
    }


    #endregion

    #region Methods

    public void OnLoad()
    {
        InitializePerfil(Resume.Perfil);

        //Restore has changes to false
        HasChanges = false;
    }

    private bool CanSave()
    {
        return PerfilWrapper != null && !PerfilWrapper.HasErrors && HasChanges;
    }

    private async void OnSave()
    
    {
        PerfilWrapper.Validate();
        if (!PerfilWrapper.HasErrors)
        {
            //Workaround
            Resume.Nombres = PerfilWrapper.Nombres;
            Resume.Apellidos = PerfilWrapper.Apellidos;
            Resume.FotoString = PerfilWrapper.FotoString;
            Resume.Perfil = PerfilWrapper.Model;

            await _resumeRepository.SaveAsync();
            HasChanges = _resumeRepository.HasChanges();
            _alertDialogViewModel.Message = "Se guardaron los cambios correctamente.";
            _dialogService.OpenDialog(_alertDialogViewModel);
        }
    }

    private async void OnNavigateToEditResume(int id)
    {
        Resume = await _resumeRepository.GetResumeWithProfile(id);

        if (Resume?.Perfil == null)
            Resume.Perfil = new Perfil();
    }

    private void InitializePerfil(Perfil perfil)
    {
        PerfilWrapper = new PerfilWrapper(perfil);
        PerfilWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _resumeRepository.HasChanges();

            if (e.PropertyName == nameof(PerfilWrapper.HasErrors))
                SaveCommand.RaiseCanExecuteChanged();
        };

        SaveCommand.RaiseCanExecuteChanged();

        //Workaround
        PerfilWrapper.Nombres = Resume.Nombres;
        PerfilWrapper.Apellidos = Resume.Apellidos;
        PerfilWrapper.FotoString = Resume.FotoString;
    }
    
    public void OnSelectionChanged()
    {
        HasChanges = true;
    }

    private void OnAgregarFoto()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            string var = ImageToBase64(openFileDialog.FileName);

            PerfilWrapper.FotoString = PerfilWrapper.Model.FotoString = Resume.FotoString = var;
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

    #endregion
}
