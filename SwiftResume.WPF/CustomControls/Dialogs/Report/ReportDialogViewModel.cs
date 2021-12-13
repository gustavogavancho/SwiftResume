﻿using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.Events;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using Model = SwiftResume.COMMON.Models;

namespace SwiftResume.WPF.CustomControls.Dialogs.Report;

public class ReportDialogViewModel : DialogViewModelBase<Model.Resume>
{
    #region Fields
    private readonly IResumeRepository _resumeRepository;

    #endregion

    #region Properties

    private Model.Resume _resume;
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

    public DelegateCommand<IDialogWindow> CancelCommand { get; private set; }
    public DelegateCommand<IDocumentPaginatorSource> PrintCommand { get; private set; }
    public DelegateCommand<string> OpenHyperlinkCommand { get; private set; }

    #endregion

    #region Constructor
    public ReportDialogViewModel(IEventAggregator eventAggregator, 
        IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;

        eventAggregator.GetEvent<NavigateToReportResume>()
            .Subscribe(OnNavigateToReportResume);

        CancelCommand = new DelegateCommand<IDialogWindow>(OnCancel);
        PrintCommand = new DelegateCommand<IDocumentPaginatorSource>(OnPrint);
        OpenHyperlinkCommand = new DelegateCommand<string>(OnHyperlink);
    }

    #endregion

    #region Methods

    private async void OnNavigateToReportResume(int id)
    {
        Resume = await _resumeRepository.GetResumeWithProfile(id);
    }

    private void OnPrint(IDocumentPaginatorSource flowDocument)
    {
        PrintDialog pd = new();
        if (pd.ShowDialog() == true)
        {
            pd.PrintDocument(flowDocument.DocumentPaginator, "FlowDocument");
        }
    }

    private void OnHyperlink(string uri)
    {
        Process.Start(new ProcessStartInfo("cmd", $"/c start {uri}") { CreateNoWindow = true });
    }

    private void OnCancel(IDialogWindow window)
    {
        CloseDialogWithResult(window, null);
    }

    #endregion
}