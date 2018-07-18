using System;
using System.Windows.Controls;
using APIGiraffe.UI.ViewModels;
using Ninject;

namespace APIGiraffe.UI.Navigation
{
    public class NavigationHelper : INavigationHelper
    {
        private IKernel _kernel;

        public NavigationHelper(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void DestroyModal()
        {
            var vm = GetMainWindowViewModel();
            vm.HideDialog();
        }

        public void NavigateTo<TUserControl, TViewModel>() 
            where TUserControl : UserControl where TViewModel : BasePageViewModel
        {
            var page = _kernel.Get<TUserControl>();
            var vm = _kernel.Get<TViewModel>();

            NavigateTo(page, vm);
        }

        public void NavigateTo<TUserControl, TViewModel>(Action<TViewModel> preNavigationAction)
          where TUserControl : UserControl where TViewModel : BasePageViewModel
        {
            var page = _kernel.Get<TUserControl>();
            var vm = _kernel.Get<TViewModel>();

            preNavigationAction.Invoke(vm);
            NavigateTo(page, vm);
        }

        public void ShowModal<TUserControl, TViewModel>()
        where TUserControl : UserControl where TViewModel : BasePageViewModel
        {
            var page = _kernel.Get<TUserControl>();
            var vm = _kernel.Get<TViewModel>();

            ShowModal(page, vm);
        }

        public void ShowModal<TUserControl, TViewModel>(Action<TViewModel> preNavigationAction)
            where TUserControl : UserControl where TViewModel : BasePageViewModel
        {
            var page = _kernel.Get<TUserControl>();
            var vm = _kernel.Get<TViewModel>();

            preNavigationAction.Invoke(vm);
            ShowModal(page, vm);
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

        private void ShowModal(UserControl content, BasePageViewModel vm)
        {
            var mainWindowVm = GetMainWindowViewModel();
            mainWindowVm.HideDialog();
            content.DataContext = vm;
            mainWindowVm.ShowDialog(content);
        }

        private void NavigateTo(UserControl page, BasePageViewModel vm)
        {
            var mainWindowVm = GetMainWindowViewModel();
            mainWindowVm.HideDialog();
            page.DataContext = vm;
            mainWindowVm.CurrentPage = page;
            mainWindowVm.Title = vm.Title;
        }
    }
}
