using System;
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest;
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels.Requests
{
    public class RenameRequestViewModel : NewItemViewModel
    {
        public override string Title => "Rename Group";

        private readonly IRenameRequestCommand _command;

        private readonly INavigationHelper _navigation;

        private int _groupId;

        public RenameRequestViewModel(INavigationHelper navigation, IRenameRequestCommand command) 
            : base()
        {
            _command = command;
            _navigation = navigation;
        }

        public void SetValues(int id, string currentName)
        {
            _groupId = id;
            ItemName = currentName;
        }

        public override void OnValidationComplete()
        {
            _command.Execute(_groupId, ItemName);

            _navigation.DestroyModal();
            _navigation.RefreshMenu();
        }

        public override void Validate()
        {
            ItemNameErrorMessage = string.Empty;
            ItemNameErrorMessage = string.Empty;
            IsValid = true;

            if (string.IsNullOrWhiteSpace(ItemName))
            {
                ItemNameErrorMessage = "Request name cannot be empty";
                IsValid = false;
            }
        }
    }
}
