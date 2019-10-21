using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace I7BAC.Command
{
    public class PatientSelectionCommand : ICommand
    {
        public Action PatientSelectionAction;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PatientSelectionAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
