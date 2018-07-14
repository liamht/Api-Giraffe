using System;
using System.Windows.Input;

namespace APIGirrafe.UI.ViewModels
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

    public class ActionCommand<T> : ICommand
    { 
        public event EventHandler CanExecuteChanged;
        private readonly Action<T> _toExecute;

        public ActionCommand(Action<T> toExecute)
        {
            _toExecute = toExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var param = (T)parameter;
            _toExecute.Invoke(param);
        }
    }
}
