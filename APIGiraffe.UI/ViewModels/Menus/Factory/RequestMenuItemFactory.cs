using System;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels.Requests;
using APIGiraffe.UI.Views;

namespace APIGiraffe.UI.ViewModels.Menus.Factory
{
    public class RequestMenuItemFactory : IRequestMenuItemFactory
    {
        private INavigationHelper _navigationHelper;

        public RequestMenuItemFactory(INavigationHelper navigationHelper)
        {
            _navigationHelper = navigationHelper;
        }

        public RequestMenuItem Create(string name, int id)
        {
            return new RequestMenuItem(name, id, GetCurrentPageAction(id),
                GetRenameRequestAction(id, name), GetDeleteRequestAction(id));
        }

        private Action GetDeleteRequestAction(int id)
        {
            return () =>
                _navigationHelper.ShowModal<DeleteRequestDialog, DeleteRequestViewModel>(vm => vm.LoadValues(id));
        }

        private Action GetRenameRequestAction(int id, string name)
        {
            return () =>
                _navigationHelper.ShowModal<RenameRequestDialog, RenameRequestViewModel>(vm => vm.SetValues(id, name));
        }

        private Action GetCurrentPageAction(int id)
        {
            return () =>
                _navigationHelper.NavigateTo<CurrentRequestPage, CurrentRequestViewModel>(vm => { vm.LoadValues(id); });
        }
    }
}
