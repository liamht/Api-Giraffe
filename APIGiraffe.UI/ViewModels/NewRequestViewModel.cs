using System;
using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels
{
    public class NewRequestViewModel : NewItemViewModel
    {
        public override string Title => "Create New Request";

        private readonly IAddNewRequestCommand _command;

        private readonly INavigationHelper _navigation;

        private int _groupId;

        public NewRequestViewModel(INavigationHelper navigation, IAddNewRequestCommand command) 
            : base()
        {
            _command = command;
            _navigation = navigation;
        }

        public void SetGroupId(int groupId)
        {
            _groupId = groupId;
        }

        public override void OnValidationComplete()
        {
            if (_groupId == 0)
            {
                throw new ArgumentException("group for request not set");
            }

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
