using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels.RequestGroups
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

        public override void OnValidationComplete()
        {
            _command.Execute(ItemName);

            _navigation.DestroyModal();
            _navigation.RefreshMenu();
        }

        public override void Validate()
        {
            IsValid = true;
            ItemNameErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(ItemName))
            {
                ItemNameErrorMessage = "Group name cannot be empty";
                IsValid = false;
            }
        }
    }
}
