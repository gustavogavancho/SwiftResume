using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;
using System;

namespace SwiftResume.WPF.ViewModels.Factories
{
    public class SwiftResumeViewModelFactory : ISwiftResumeViewModelFactory
    {
        private readonly CreateViewModel<ResumeViewModel> _createResumeViewModel;

        public SwiftResumeViewModelFactory(CreateViewModel<ResumeViewModel> createResumeViewModel)
        {
            _createResumeViewModel = createResumeViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Resume:
                    return _createResumeViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
