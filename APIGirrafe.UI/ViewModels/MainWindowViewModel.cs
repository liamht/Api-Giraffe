using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using APIGirrafe.UI.ViewModels.Menus;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;
using Menu = APIGirrafe.UI.ViewModels.Menus.Menu;
using APIGirrafe.ApplicationServices.Requests.Queries.GetRequestGroups;
using APIGirrafe.UI.ViewModels.Commands;
using APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup;

namespace APIGirrafe.UI.ViewModels
{
    public class MainWindowViewModel : NotifyableViewModel
    {
        private readonly INavigationHelper _navigation;
        private readonly IGetRequestGroupsQuery _getGroupsQuery;
        private readonly IDeleteRequestGroupCommand _deleteGroupCommand;

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

        public ICommand NewRequestCommand { get; set; }

        #endregion

        public MainWindowViewModel(IGetRequestGroupsQuery getGroupsQuery, IDeleteRequestGroupCommand deleteGroupCommand, INavigationHelper navigation)
        {
            _navigation = navigation;
            _getGroupsQuery = getGroupsQuery;
            _deleteGroupCommand = deleteGroupCommand;

            NewRequestCommand = new ActionCommand(() => _navigation.ShowModal<NewRequestDialog, NewRequestViewModel>());
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

            var menuGroups = groups.Select(group => new MenuGroup(group.Name,
                GetNewRequestDialogAction(group.Id),
                GetDeleteGroupAction(group.Id),
                group.Requests.Select(request => new RequestMenuItem(request.Name, request.Id, GetNavigateToCurrentRequestPageAction(request.Id, request.Name))).ToArray()
            ));

            Menu = new Menu()
            {
                Groups = new ObservableCollection<MenuGroup>(menuGroups)
            };
        }

        private Action GetNewRequestDialogAction(int requestGroupId)
        {
            return () =>
            {
                _navigation.ShowModal<NewRequestDialog, NewRequestViewModel>(vm => vm.SetGroupId(requestGroupId));
            };
        }

        private Action GetDeleteGroupAction(int requestGroupId)
        {
            return () =>
            {
                _deleteGroupCommand.Execute(requestGroupId);
                _navigation.RefreshMenu();
            };
        }

        private Action GetNavigateToCurrentRequestPageAction(int requestId, string requestName)
        {
            return () =>
            {
                _navigation.NavigateTo<CurrentRequestPage, CurrentRequestViewModel>(vm =>
                {
                    vm.Name = requestName;
                    vm.LoadValues(requestId);
                });
            };
        }
    }
}

