using System;
using System.Windows.Input;

namespace APIGirrafe.UI.ViewModels.Commands
{
    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<int> _toExecute;

        public DeleteCommand(Action<int> toExecute)
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
