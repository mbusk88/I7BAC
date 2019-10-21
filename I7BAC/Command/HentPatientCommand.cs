using System;
using System.Windows.Input;

namespace I7BAC.Command
{
    public class HentPatientCommand : ICommand
    {
        public Action HentPatientAction;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HentPatientAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
