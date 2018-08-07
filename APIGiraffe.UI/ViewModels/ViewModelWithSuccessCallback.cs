using APIGiraffe.UI.ViewModels.Commands;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels
{
    public abstract class ViewModelWithSuccessCallback : BasePageViewModel
    {
        public ICommand OnConfirmCommand => new ActionCommand(OnSuccess);

        public abstract void OnSuccess();
    }
}
