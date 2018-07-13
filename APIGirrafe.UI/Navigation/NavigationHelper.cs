using System;
using System.Windows.Controls;
using APIGirrafe.UI.ViewModels;

namespace APIGirrafe.UI.Navigation
{
    public class NavigationHelper : INavigationHelper
    {
        public NavigationHelper()
        {
        }

        public void DestroyModal()
        {
            var vm = GetMainWindowViewModel();
            vm.HideDialog();
        }

        public void NavigateTo(UserControl page, BasePageViewModel vm)
        {
            var mainWindowVm = GetMainWindowViewModel();
            mainWindowVm.HideDialog();
            page.DataContext = vm;
            mainWindowVm.CurrentPage = page;
            mainWindowVm.Title = vm.Title;
        }

        public void ShowModal(UserControl content, BasePageViewModel vm)
        {
            var mainWindowVm = GetMainWindowViewModel();
            mainWindowVm.HideDialog();
            content.DataContext = vm;
            mainWindowVm.ShowDialog(content);
        }

        public void RefreshMenu()
        {
            GetMainWindowViewModel().LoadMenu();
        }

        private MainWindowViewModel GetMainWindowViewModel()
        {
            var vm = System.Windows.Application.Current?.MainWindow?.DataContext as MainWindowViewModel;

            if (vm == null)
            {
                throw new Exception("Navigation will not work unless Main Window of the application has the binding of MainWindowViewModel");
            }

            return vm;
        }
    }
}
