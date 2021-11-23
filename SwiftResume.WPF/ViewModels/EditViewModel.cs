using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.Events;
using SwiftResume.WPF.State.Navigators;

namespace SwiftResume.WPF.ViewModels;

public class EditViewModel : ViewModelBase
{
    #region Fields

    private readonly ViewModelDelegateRenavigator<ResumeViewModel> _resumeRenavigator;
    private readonly IResumeRepository _resumeRepository;

    #endregion

    #region Properties
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
    public DelegateCommand ReturnCommand { get; private set; }
    public DelegateCommand SaveCommand { get; set; }
    #endregion

    #region Constructor

    public EditViewModel(ViewModelDelegateRenavigator<ResumeViewModel> resumeRenavigator,
        IEventAggregator eventAggregator,
        IResumeRepository resumeRepository)
    {
        _resumeRenavigator = resumeRenavigator;
        _resumeRepository = resumeRepository;

        eventAggregator.GetEvent<NavigateToEditResume>()
            .Subscribe(OnNavigateToEditResume);

        ReturnCommand = new DelegateCommand(OnReturn);
        SaveCommand = new DelegateCommand(OnSave);
    }

    private async void OnSave()
    {
        var check = Resume;
        await _resumeRepository.SaveAsync();
    }

    private async void OnNavigateToEditResume(NavigateToEditResumeArgs model)
    {
        Resume = await _resumeRepository.GetResumeWithProfile(model.Id);
    }

    private void OnReturn()
    {
        _resumeRenavigator.Renavigate();
    }

    #endregion

    #region Methods
    public void OnLoad()
    {
        
    }
    #endregion
}
