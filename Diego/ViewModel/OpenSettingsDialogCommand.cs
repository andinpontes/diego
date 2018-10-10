using Diego.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Diego.ViewModel
{
    public class OpenSettingsDialogCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SettingsDialog dialog = new SettingsDialog
            {
                Owner = Application.Current.MainWindow
            };

            dialog.ShowDialog();
        }
    }
}
