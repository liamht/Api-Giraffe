using System;
using System.Collections.Generic;
using APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequestGroup;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.Views;

namespace APIGiraffe.UI.ViewModels.Menus.Factory
{
    public class MenuGroupFactory: IMenuGroupFactory
    {
        private readonly INavigationHelper _navigationHelper;
        private readonly IDeleteRequestGroupCommand _deleteGroupCommand;

        public MenuGroupFactory(INavigationHelper navigationHelper, IDeleteRequestGroupCommand deleteGroupCommand)
        {
            _navigationHelper = navigationHelper;
            _deleteGroupCommand = deleteGroupCommand;
        }

        public MenuGroup Create(string name, int id, List<RequestMenuItem> items)
        {
            return new MenuGroup(name,
                GetNewRequestDialogAction(id),
                GetDeleteGroupAction(id),
                GetRenameGroupAction(id, name),
                items.ToArray()
            );
        }

        private Action GetRenameGroupAction(int id, string name)
        {
            return () =>
            {
                _navigationHelper.ShowModal<RenameRequestGroupDialog, RenameGroupViewModel>(vm => vm.SetValues(id, name));
            };
        }

        private Action GetNewRequestDialogAction(int requestGroupId)
        {
            return () =>
            {
                _navigationHelper.ShowModal<NewRequestDialog, NewRequestViewModel>(vm => vm.SetGroupId(requestGroupId));
            };
        }

        private Action GetDeleteGroupAction(int requestGroupId)
        {
            return () =>
            {
                _deleteGroupCommand.Execute(requestGroupId);
                _navigationHelper.RefreshMenu();
            };
        }
    }
}
