using APIGiraffe.UI.ViewModels;
using System;
using System.Windows.Controls;

namespace APIGiraffe.UI.Navigation
{
    public interface INavigationHelper
    {
        void NavigateTo<TUserControl, TViewModel>() where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void NavigateTo<TUserControl, TViewModel>(Action<TViewModel> preNavigationAction) where TUserControl : UserControl where TViewModel : BasePageViewModel;
        
        void ShowModal<TUserControl, TViewModel>() where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void ShowModal<TUserControl, TViewModel>(Action<TViewModel> preNavigationAction) where TUserControl : UserControl where TViewModel : BasePageViewModel;

        void DestroyModal();

        void RefreshMenu();
    }
}