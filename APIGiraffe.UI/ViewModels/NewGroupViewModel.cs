using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels
{
    public class NewGroupViewModel : NewItemViewModel
    {
        public override string Title => "Create New Group";

        private readonly IAddNewRequestGroupCommand _command;

        private readonly INavigationHelper _navigation;

        public NewGroupViewModel(INavigationHelper navigation, IAddNewRequestGroupCommand command) 
            : base()
        {
            _command = command;
            _navigation = navigation;
        }

        public override void OnSuccess()
        {
            _command.Execute(ItemName);

            _navigation.DestroyModal();
            _navigation.RefreshMenu();
        }
    }
}
