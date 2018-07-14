using System;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequest;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public class NewRequestViewModel : NewItemViewModel
    {
        public override string Title => "Create New Request";

        private readonly IAddNewRequestCommand _command;

        private int _groupId;

        public NewRequestViewModel(INavigationHelper navigation, IAddNewRequestCommand command) 
            : base(navigation)
        {
            _command = command;
        }

        public void SetGroupId(int groupId)
        {
            _groupId = groupId;
        }

        public override void OnSuccess()
        {
            if (_groupId == 0)
            {
                throw new ArgumentException("group for request not set");
            }

            _command.Execute(_groupId, ItemName);
            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
