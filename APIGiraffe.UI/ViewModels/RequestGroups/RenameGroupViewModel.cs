using System;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.RenameRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels.RequestGroups
{
    public class RenameGroupViewModel : NewItemViewModel
    {
        public override string Title => "Rename Group";

        private readonly IRenameRequestGroupCommand _command;
    
        private readonly INavigationHelper _navigation;

        private int _groupId;

        public RenameGroupViewModel(INavigationHelper navigation, IRenameRequestGroupCommand command) 
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
                ItemNameErrorMessage = "Group name cannot be empty";
                IsValid = false;
            }
        }
    }
}
