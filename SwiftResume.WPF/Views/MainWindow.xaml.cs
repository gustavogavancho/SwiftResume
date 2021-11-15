using SwiftResume.WPF.ViewModels;
using System.Windows;

namespace SwiftResume.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel dataContext)
    {
        InitializeComponent();

        DataContext = dataContext;
    }
}