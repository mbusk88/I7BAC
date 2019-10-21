using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace I7BAC.Command
{
    public class RekvisionSelectionCommand : ICommand
    {
        public Action RekvisitionSelectionAction;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RekvisitionSelectionAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
