using System;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels;
using APIGiraffe.UI.ViewModels.Menus;
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

        public RequestMenuItem Create(string text, int id)
        {
            return new RequestMenuItem(text, id, GetCurrentPageAction(id));
        }

        private Action GetCurrentPageAction(int id)
        {
            return () =>
                _navigationHelper.NavigateTo<CurrentRequestPage, CurrentRequestViewModel>(vm => { vm.LoadValues(id); });
        }
    }
}
