using System;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels.Commands
{
    public class ActionCommandWithId : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<int> _toExecute;

        public ActionCommandWithId(Action<int> toExecute)
        {
            _toExecute = toExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var param = (int)parameter;
            _toExecute.Invoke(param);
        }
    }
}
