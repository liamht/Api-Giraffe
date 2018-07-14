using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequestGroup;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public class NewGroupViewModel : NewItemViewModel
    {
        public override string Title => "Create New Group";

        private readonly IAddNewRequestGroupCommand _command;

        public NewGroupViewModel(INavigationHelper navigation, IAddNewRequestGroupCommand command) 
            : base(navigation)
        {
            _command = command;
        }

        public override void OnSuccess()
        {
            _command.ExecuteAsync(ItemName);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
