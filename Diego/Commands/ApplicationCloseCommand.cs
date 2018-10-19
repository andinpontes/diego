using System;
using System.Windows;
using System.Windows.Input;

namespace Diego.Commands
{
    public class ApplicationCloseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (Application.Current == null)
            {
                return false;
            }

            if (Application.Current.MainWindow == null)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
