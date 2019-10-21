using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace I7BAC.Command
{
    public class ImageChangedCommand: ICommand
    {
        public Action ImageChangedAction;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ImageChangedAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
