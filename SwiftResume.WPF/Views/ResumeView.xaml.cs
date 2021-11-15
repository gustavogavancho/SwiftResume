using System.Windows.Controls;

namespace SwiftResume.WPF.Views;

public partial class ResumeView : UserControl
{
    public ResumeView()
    {
        InitializeComponent();
    }

    private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var checkSender = sender;
        var checkE = sender;
    }
}
