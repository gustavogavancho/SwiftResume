using Prism.Events;
using SwiftResume.BIZ.Repositories;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.Events;
using System.Collections.ObjectModel;
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

    public List<Model.Habilidad> Idiomas { get; set; }
    public List<Model.Habilidad> Habilidades { get; set; }
    public List<Model.Habilidad> Software { get; set; }

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
        Resume = await _resumeRepository.GetResumeWithProfileHabilidades(id);
        Idiomas = Resume.Habilidades.Where(x => x.Tipo == "Idioma").ToList();
        Habilidades = Resume.Habilidades.Where(x => x.Tipo == "Habilidad").ToList();
        Software = Resume.Habilidades.Where(x => x.Tipo == "Software").ToList();
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