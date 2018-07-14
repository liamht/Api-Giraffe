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
using System.Threading.Tasks;
using APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup;

namespace APIGirrafe.UI.ViewModels
{
    public class MainWindowViewModel : NotifyableViewModel
    {
        public event EventHandler OnNewGroupButtonClicked;

        private readonly INavigationHelper _navigation;
        private readonly IGetRequestGroupsQuery _getGroupsQuery;
        private readonly IDeleteRequestGroupCommand _deleteGroupCommand;
        private readonly Func<NewRequestViewModel> _newRequestViewModelCreator;
        private readonly Func<CurrentRequestViewModel> _currentRequestViewModelCreator;

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

        public MainWindowViewModel(IGetRequestGroupsQuery getGroupsQuery, IDeleteRequestGroupCommand deleteGroupCommand, INavigationHelper navigation, 
            Func<NewRequestViewModel> newRequestInitiator, Func<CurrentRequestViewModel> currentRequestInitiator)
        {
            _navigation = navigation;
            _getGroupsQuery = getGroupsQuery;
            _deleteGroupCommand = deleteGroupCommand;
            _newRequestViewModelCreator = newRequestInitiator;
            _currentRequestViewModelCreator = currentRequestInitiator;

            NewRequestCommand = new ActionCommand(() => OnNewGroupButtonClicked?.Invoke(this, new EventArgs()));
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
                var vm = _newRequestViewModelCreator.Invoke();
                vm.SetGroupId(requestGroupId);
                _navigation.ShowModal(new NewRequestDialog(), vm);
            };
        }

        private Action GetDeleteGroupAction(int requestGroupId)
        {
            return async () =>
            {
                await _deleteGroupCommand.ExecuteAsync(requestGroupId);
                _navigation.RefreshMenu();
            };
        }

        private Action GetNavigateToCurrentRequestPageAction(int requestId, string requestName)
        {
            return () =>
            {
                var vm = _currentRequestViewModelCreator.Invoke();
                vm.Name = requestName;
                _navigation.NavigateTo(new CurrentRequestPage(), vm);
                vm.LoadValues(requestId);
            };
        }
    }
}

