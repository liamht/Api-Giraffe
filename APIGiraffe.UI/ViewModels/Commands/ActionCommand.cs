using System;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels.Commands
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _toExecute;

        public ActionCommand(Action toExecute)
        {
            _toExecute = toExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _toExecute.Invoke();
        }
    }
}
