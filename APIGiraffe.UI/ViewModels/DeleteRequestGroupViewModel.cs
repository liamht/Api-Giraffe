using APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequestGroup;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels.Commands;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels
{
    public class DeleteRequestGroupViewModel : BasePageViewModel
    {
        public override string Title => "Delete Request";

        public ICommand ConfirmDeleteCommand { get; private set; }

        public int Id { get; private set; }

        public DeleteRequestGroupViewModel(IDeleteRequestGroupCommand command, INavigationHelper navigationHelper)
        {
            ConfirmDeleteCommand = new ActionCommand(() =>
            {
                command.Execute(Id);
                navigationHelper.RefreshMenu();
            });
        }

        public void LoadValues(int id)
        {
            Id = id;
        }
    }
}
