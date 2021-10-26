using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;
using System;

namespace SwiftResume.WPF.ViewModels.Factories
{
    public class SwiftResumeViewModelFactory : ISwiftResumeViewModelFactory
    {
        private readonly CreateViewModel<ResumeViewModel> _createResumeViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;

        public SwiftResumeViewModelFactory(CreateViewModel<ResumeViewModel> createResumeViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel)
        {
            _createResumeViewModel = createResumeViewModel;
            _createLoginViewModel = createLoginViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Resume:
                    return _createResumeViewModel();
                case ViewType.Login:
                    return _createLoginViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
