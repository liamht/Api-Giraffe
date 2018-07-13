using APIGirrafe.UI.ViewModels;
using System.Windows.Controls;

namespace APIGirrafe.UI.Navigation
{
    public interface INavigationHelper
    {
        void NavigateTo(UserControl page, BasePageViewModel vm);

        void ShowModal(UserControl content, BasePageViewModel vm);
        
        void DestroyModal();

        void RefreshMenu();
    }
}