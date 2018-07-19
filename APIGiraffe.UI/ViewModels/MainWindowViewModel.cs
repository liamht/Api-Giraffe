using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using APIGiraffe.UI.ViewModels.Menus;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.Views;
using Menu = APIGiraffe.UI.ViewModels.Menus.Menu;
using APIGiraffe.ApplicationServices.Requests.Queries.GetRequestGroups;
using APIGiraffe.UI.ViewModels.Commands;
using APIGiraffe.UI.ViewModels.Menus.Factory;

namespace APIGiraffe.UI.ViewModels
{
    public class MainWindowViewModel : NotifyableViewModel
    {
        private readonly IGetRequestGroupsQuery _getGroupsQuery;
        private readonly IMenuGroupFactory _menuGroupFactory;

        #region Properties

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
        }

        private UserControl _currentDialog;
        public UserControl CurrentDialog
        {
            get => _currentDialog;
            set
            {
                _currentDialog = value;
                NotifyPropertyChanged(nameof(CurrentDialog));
            }
        }

        private bool _isDialogShowing;
        public bool IsDialogShowing
        {
            get => _isDialogShowing;
            set
            {
                _isDialogShowing = value;
                NotifyPropertyChanged(nameof(IsDialogShowing));
            }
        }

        private Menu _menu;
        public Menu Menu
        {
            get => _menu;
            set
            {
                _menu = value;
                NotifyPropertyChanged(nameof(Menu));
            }
        }

        private readonly IRequestMenuItemFactory _menuItemFactory;

        public ICommand NewRequestCommand { get; set; }

        #endregion

        public MainWindowViewModel(INavigationHelper navigation, IGetRequestGroupsQuery getGroupsQuery, 
            IMenuGroupFactory menuGroupFactory, IRequestMenuItemFactory menuItemFactory)
        {
            _getGroupsQuery = getGroupsQuery;
            _menuGroupFactory = menuGroupFactory;
            _menuItemFactory = menuItemFactory;

            NewRequestCommand = new ActionCommand(navigation.ShowModal<NewRequestDialog, NewRequestViewModel>);
        }

        public void ShowDialog(UserControl dialogContent)
        {
            CurrentDialog = dialogContent;
            IsDialogShowing = true;
        }

        public void HideDialog()
        {
            CurrentDialog = null;
            IsDialogShowing = false;
        }

        public void LoadMenu()
        {
            var groups = _getGroupsQuery.Execute();

            var menuGroups = groups.Select(group =>
            {
                var menuItems = group.Requests.Select(c => _menuItemFactory.Create(c.Name, c.Id)).ToList();
                return _menuGroupFactory.Create(group.Name, group.Id, menuItems);
            });

            Menu = new Menu()
            {
                Groups = new ObservableCollection<MenuGroup>(menuGroups)
            };
        }
    }
}

