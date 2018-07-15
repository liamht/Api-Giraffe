using System.Collections.ObjectModel;

namespace APIGiraffe.UI.ViewModels.Menus
{
    public class Menu : NotifyableViewModel
    {
        public ObservableCollection<MenuGroup> Groups { get; set; }

        private MenuGroup _selectedMenuGroup;
        public MenuGroup SelectedMenuGroup
        {
            get => _selectedMenuGroup;
            set
            {
                _selectedMenuGroup = value;
                NotifyPropertyChanged(nameof(SelectedMenuGroup));
            }
        }
    }
}
