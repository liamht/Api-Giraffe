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

        public override void OnSuccess()
        {
            if (_groupId == 0)
            {
                throw new ArgumentException("group for request not set");
            }

            _command.Execute(_groupId, ItemName);
            _navigation.DestroyModal();
            _navigation.RefreshMenu();
        }
    }
}
