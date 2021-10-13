using SwiftResume.WPF.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SwiftResume.WPF.Commands
{
    public class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CloseWindowCommand()
        {

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Close();
        }
    }
}
