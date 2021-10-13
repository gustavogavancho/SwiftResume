using SwiftResume.WPF.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace SwiftResume.WPF.Commands
{
    public class MinimizeWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public MinimizeWindowCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().WindowState = WindowState.Minimized;
        }
    }
}
