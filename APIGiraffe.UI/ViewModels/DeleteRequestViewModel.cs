using APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequest;
using APIGiraffe.UI.ViewModels.Commands;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels
{
    public class DeleteRequestViewModel
    {
        public ICommand ConfirmDeleteCommand { get; private set; }

        public int Id { get; private set; }

        public DeleteRequestViewModel(IDeleteRequestCommand command)
        {
            ConfirmDeleteCommand = new ActionCommand(() => command.Execute(Id));
        }

        public void LoadValues(int id)
        {
            Id = id;
        }
    }
}
