using APIGirrafe.UI.ViewModels;
using System;
using System.Windows.Controls;

namespace APIGirrafe.UI.Navigation
{
    public interface INavigationHelper
    {
        [Obsolete("Consider passing the type of usercontrol and viewmodel as arguments so the navigation helper can create new instances of the objects itself")]
        void NavigateTo(UserControl page, BasePageViewModel vm);

        void NavigateTo<TUserControl, TViewModel>() where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void NavigateTo<TUserControl, TViewModel>(Action<TViewModel> postNavigationAction) where TUserControl : UserControl where TViewModel : BasePageViewModel;

        [Obsolete("Consider passing the type of usercontrol and viewmodel as arguments so the navigation helper can create new instances of the objects itself")]
        void ShowModal(UserControl content, BasePageViewModel vm);

        void ShowModal<TUserControl, TViewModel>() where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void ShowModal<TUserControl, TViewModel>(Action<TViewModel> postNavigationAction) where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void DestroyModal();

        void RefreshMenu();
    }
}