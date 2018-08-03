﻿using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest;
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels
{
    public class RenameRequestViewModel : NewItemViewModel
    {
        public override string Title => "Rename Group";

        private readonly IRenameRequestCommand _command;

        private int _groupId;

        public RenameRequestViewModel(INavigationHelper navigation, IRenameRequestCommand command) 
            : base(navigation)
        {
            _command = command;
        }

        public void SetValues(int id, string currentName)
        {
            _groupId = id;
            ItemName = currentName;
        }

        public override void OnSuccess()
        {
            _command.Execute(_groupId, ItemName);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}