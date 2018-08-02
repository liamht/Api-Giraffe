using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using APIGiraffe.UI.ViewModels.Menus;
using Menu = APIGiraffe.UI.ViewModels.Menus.Menu;

namespace APIGiraffe.UI.DesignTimeViewModels
{
    public class MainWindowViewModel
    {
        public string Title => "API Giraffe";

        public RequestMenuItem SelectedRequestMenuItem { get; set; }

        public MenuGroup SelectedMenuGroup { get; set; }

        public Menu Menu { get; set; }

        public UserControl CurrentPage { get; set; }

        public UserControl CurrentDialog { get; set; }

        public bool IsDialogShowing { get; set; }

        public ICommand NewRequestCommand { get; set; }

        public MainWindowViewModel()
        {
            Menu = new Menu()
            {
                Groups = new ObservableCollection<MenuGroup>()
                {
                    new MenuGroup("LIVE", null, null, null,
                        new RequestMenuItem("Get Users", 1, () => {}, () => { }),
                        new RequestMenuItem("Get User Stats", 1, () => {}, () => { }),
                        new RequestMenuItem("Get User Comments", 1, () => {}, () => { })
                        )
                }
            };
        }
    }
}

